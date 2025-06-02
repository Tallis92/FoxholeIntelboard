document.addEventListener("DOMContentLoaded", () => {
    const list = [];
    const productionCosts = window.productionCosts || [];
    const flatProductionCosts = productionCosts.flat();
    const resources = window.resources || [];
    const materials = window.materials || [];

    // Checks for existing items in the list, if they exist it just increases the values,
    // if not it will create a new item and with amounts set to 1.
    function addToList(id, name, type) {
        const existing = list.find(c => c.id === id && c.type === type);
        if (existing) {
            existing.amount++;
            existing.requiredAmount++;
        } else {
            list.push({ id, name, type, amount: 1, requiredAmount: 1});
        }
        updateListUI();
    }

    function removeFromList(id, type) {
        const index = list.findIndex(c => c.id === id && c.type === type);
        if (index !== -1) {
            list[index].amount--;
            if (list[index].amount <= -1) {
                list.splice(index, 1);
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

            input.onchange = () => {
                const val = parseInt(input.value, 10);
                if (isNaN(val)) return input.value = item.amount;
                if (val <= 0) {
                    const idx = list.findIndex(i => i.id === item.id && i.type === item.type);
                    list.splice(idx, 1);
                } else {
                    item.amount = val;
                }
                updateListUI();
            };

            li.appendChild(label);
            li.appendChild(input);
            display.appendChild(li);
        });

        document.getElementById("SelectedItems").value = JSON.stringify(list);
        updateCostUI();
    }

    // Goes through the items ProductionCost in an outer loop, in the innerloop it loops through the subcosts with the names and amounts of materials
    // or resources. It then calculates the requiredAmounts total costs and displays the inventory's total costs.
    function updateCostUI() {
        const costDisplay = document.getElementById("costDisplay");
        costDisplay.innerHTML = "";

        const materialTotals = {};
        const resourceTotals = {};

        console.log("Checking List: ", list);
        list.forEach(item => {

            console.log("Checking Item: ", item);
            const costs = flatProductionCosts.filter(c => c.craftableItemId === item.id);
            console.log("Checking productionCosts", flatProductionCosts);
            console.log("Checking cost", costs);

            if (costs.length === 0) {
                console.log("Cost not found");
            } else {
                console.log("Cost exists");
                costs.forEach(cost => {  
                    cost.productionCost.forEach(subCost => {

                        if (subCost.materialId != null) {
                            const materialName = materials.find(m => m.id === subCost.materialId)?.name;
                            const materialCrateAmount = materials.find(m => m.id === subCost.materialId)?.crateAmount || 1;

                            if (materialName) {
                                if (!materialTotals[materialName]) materialTotals[materialName] = 0;
                                const materialQty = subCost.amount * item.amount;
                                materialTotals[materialName] += materialQty;

                                const materialProductionCost = flatProductionCosts.find(c => c.craftableItemId === subCost.materialId);
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
                    });
                });
            }
        });
        // Builds the costsection with specific stylings for the productionCosts.
        const buildCostSection = (title, totals) => {
            const section = document.createElement("div");
            const header = document.createElement("h5");
            header.textContent = title;
            section.appendChild(header);

            for (const [name, total] of Object.entries(totals)) {
                const row = document.createElement("div");
                row.className = "d-flex justify-content-between ps-3";
                row.innerHTML = `<span>- ${name}</span><span>${total}</span>`;
                section.appendChild(row);
            }
            return section;
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
