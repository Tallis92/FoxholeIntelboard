document.addEventListener("DOMContentLoaded", () => {
    const list = [];
    const productionCosts = window.productionCosts || [];

    function addToList(id, name, type) {
        const existing = list.find(c => c.id === id && c.type === type);
        if (existing) {
            existing.amount++;
        } else {
            list.push({ id, name, type, amount: 1 });
        }
        updateListUI();
    }

    function removeFromList(id, type) {
        const index = list.findIndex(c => c.id === id && c.type === type);
        if (index !== -1) {
            list[index].amount--;
            if (list[index].amount <= 0) {
                list.splice(index, 1);
            }
        }
        updateListUI();
    }

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
            input.min = 1;
            input.value = item.amount;
            input.className = "form-control form-control-sm";
            input.style.width = "70px";

            input.onchange = () => {
                const val = parseInt(input.value, 10);
                if (isNaN(val)) return input.value = item.amount;
                if (val <= 0) {
                    const index = list.findIndex(i => i.id === item.id && i.type === item.type);
                    list.splice(index, 1);
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

    function updateCostUI() {
        const costDisplay = document.getElementById("costDisplay");
        costDisplay.innerHTML = "";

        const materialTotals = {};
        const resourceTotals = {};

        list.forEach(item => {
            const cost = productionCosts.find(c => c.id === item.id && c.type === item.type);
            if (cost) {
                if (cost.materialName && cost.materials) {
                    if (!materialTotals[cost.materialName]) materialTotals[cost.materialName] = 0;
                    materialTotals[cost.materialName] += cost.materials * item.amount;
                }
                if (cost.resourceName && cost.resources) {
                    if (!resourceTotals[cost.resourceName]) resourceTotals[cost.resourceName] = 0;
                    resourceTotals[cost.resourceName] += cost.resources * item.amount;
                }
            }
        });

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

    // Expose functions to global scope if needed
    window.addToList = addToList;
    window.removeFromList = removeFromList;
});
