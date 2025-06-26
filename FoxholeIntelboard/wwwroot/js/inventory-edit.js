document.addEventListener("DOMContentLoaded", () => {
    const list = [];
    const productionCosts = window.productionCosts || [];
    const flatProductionCosts = productionCosts.flat();
    const resources = window.resources || [];
    const materials = window.materials || [];
    const existingItems = window.existingItems || [];

    // Loops through each item in existingItem, if it finds a match in List it sets the amount to the existing values, else it makes a shallow copy
    // and ads it to the list. Then calls updateListUI to show the changes in the UI. This is to combine both existing and new items.
    existingItems.forEach(item => {
        const existing = list.find(c => c.id === item.id && c.type === item.type);
        if (existing) {
            existing.amount += item.amount;
            existing.requiredAmount += item.requiredAmount;
        } else {
            list.push({ ...item });
        }
    });

    updateListUI();

    // Checks for existing items in the list, if they exist it just increases the values, if not it will create a new item and with amounts set to 1.
    function addToList(id, name, type) {
        const existing = list.find(c => c.id === id && c.type === type);
        if (existing) {
            existing.requiredAmount++;
        } else {
            list.push({ id, name, type, amount: 0, requiredAmount: 1});
        }
        updateListUI();
    }

    function removeFromList(id, type) {
        const index = list.findIndex(c => c.id === id && c.type === type);
        if (index !== -1) {
            list[index].amount--;
            if (list[index].amount <= -1) {
                list[index].amount = 0;
                list[index].requiredAmount -= 1;
                if (list[index].requiredAmount <= 0) {
                    list.splice(index, 1);
                }
            }
        }
        updateListUI();
    }

    // Loads up the listdisplay div, clears it then loops through each item in list to create a list of input items that can be modified.
    // Then appends everything into a new list item.
    function updateListUI() {
        const display = document.getElementById("listDisplay");
        display.innerHTML = "";
        list.forEach(item => {
            const li = document.createElement("li");
            li.className = "list-group-item d-flex justify-content-between align-items-center";

            const label = document.createElement("span");
            label.textContent = `${item.name}: `;

            const input = document.createElement("input");
            input.type = "number";
            input.min = 0;
            input.value = item.amount;
            input.className = "form-control form-control-sm";
            input.style.width = "70px";

            const span = document.createElement("span");
            span.textContent = ` / ${item.requiredAmount}`;

            input.onchange = () => {
                const val = parseInt(input.value, 10);
                if (isNaN(val)) {
                    return input.value = item.amount;
                }
                if (val <= -1) {
                    const idx = list.findIndex(i => i.id === item.id && i.type === item.type);
                    list[idx].amount = 0;
                } else {
                    item.amount = val;
                }
                updateListUI();
            };

            li.appendChild(label);
            li.appendChild(input);
            li.appendChild(span);
            display.appendChild(li);
        });

        document.getElementById("SelectedItems").value = JSON.stringify(list);
        updateCostUI();
    }

    // Builds the costsection with specific stylings for the productionCosts.
    function buildCostSection(title, totals){
        const section = document.createElement("div");
        const header = document.createElement("h5");
        header.textContent = title;
        section.appendChild(header);

        for (const [name, total] of Object.entries(totals)) {
            const row = document.createElement("div");
            row.className = "d-flex justify-content-between ps-3";
            if (total <= 0) {
                row.innerHTML = `<span>- ${name}</span><span>${0}</span>`;
            } else {
                row.innerHTML = `<span>- ${name}</span><span>${total}</span>`;
            }

            section.appendChild(row);
        }
        return section;
    };

    // Goes through the items ProductionCost in an outer loop, in the innerloop it loops through the subcosts with the names and amounts of materials
    // or resources. It then calculates the requiredAmounts total costs, calculates amounts totalcost and then subtract the amounts totalcost from the requiredAmounts totalcost
    // to dynamically change costs on the screen.
    function updateCostUI() {
        const costDisplay = document.getElementById("costDisplay");
        costDisplay.innerHTML = "";

        const materialTotals = {};
        const resourceTotals = {};
        for (const item of list) {
            const costs = productionCosts.filter(c => c.craftableItemId === item.id);
            for (const cost of costs) {
                for (const sc of cost.productionCost) {
                    if (sc.materialId) {
                        const name = materials.find(m => m.id === sc.materialId)?.name;
                        if (name && !(name in materialTotals)) materialTotals[name] = 0;
                    }
                    if (sc.resourceId) {
                        const name = resources.find(r => r.id === sc.resourceId)?.name;
                        if (name && !(name in resourceTotals)) resourceTotals[name] = 0;
                    }
                }
            }
        }
        for (const item of list) {
            const costs = productionCosts.filter(c => c.craftableItemId === item.id);

            if (costs.length === 0) {
                console.log("Cost not found");
            } else {
                for (const cost of costs) {
                    for (const subCost of cost.productionCost) {

                        if (item.amount > item.requiredAmount) {
                            continue;
                        }

                        if (subCost.materialId != null) {
                            const materialName = materials.find(m => m.id === subCost.materialId)?.name;
                            const materialCrateAmount = materials.find(m => m.id === subCost.materialId)?.crateAmount || 1;

                            if (materialName) {
                                if (!materialTotals[materialName]) materialTotals[materialName] = 0;
                                let materialQty = subCost.amount * item.requiredAmount;
                                materialQty -= (subCost.amount * item.amount);
                                materialTotals[materialName] += materialQty;

                                const materialProductionCost = productionCosts.find(c => c.craftableItemId === subCost.materialId);
                                if (materialProductionCost) {
                                    materialProductionCost.productionCost.forEach(matSubCost => {
                                        const resourceName = resources.find(r => r.id === matSubCost.resourceId)?.name;
                                        if (resourceName) {
                                            if (!resourceTotals[resourceName]) resourceTotals[resourceName] = 0;

                                            const salvageCost = matSubCost.amount * materialQty;
                                            resourceTotals[resourceName] += salvageCost;
                                        }
                                    });
                                }
                            }
                        }

                        if (subCost.resourceId != null) {
                            const resourceName = resources.find(r => r.id === subCost.resourceId)?.name;

                            if (!resourceTotals[resourceName]) resourceTotals[resourceName] = 0;

                            // Diesel requires a different calculation method so the app needs to check this specifically.
                            if (cost.name == "Diesel") {
                                resourceTotals[resourceName] += subCost.amount * 1 * item.amount;
                            }
                            else {
                                resourceTotals[resourceName] += subCost.amount * cost.crateAmount * item.amount;
                            }    
                        }
                        console.log("Totals: ", resourceTotals);
                    };
                };
            }
        };

        // Combines the different cost objects under two main categories.
        if (Object.keys(materialTotals).length > 0) {
            costDisplay.appendChild(buildCostSection("Materials", materialTotals));
        }
        if (Object.keys(resourceTotals).length > 0) {
            costDisplay.appendChild(buildCostSection("Resources", resourceTotals));
        }
    }

    // Stop dropdown click propagation
    document.querySelectorAll('.dropdown-menu').forEach(menu => {
        menu.addEventListener('click', e => e.stopPropagation());
    });

    window.addToList = addToList;
    window.removeFromList = removeFromList;
});
