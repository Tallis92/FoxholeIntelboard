document.addEventListener("DOMContentLoaded", async () => {
    const selectedCosts = document.getElementById("selectCosts");
    const resourceCostsContainer = document.getElementById("resourceCostsContainer");
    const currentValues = initialProductionCosts || [];
    let resources = [];

    // Attempts to retrieve all materials from the database using the API endpoint.
    try {
        const resp = await fetch("https://localhost:7088/api/Resource");
        if (!resp.ok) throw new Error(`HTTP ${resp.status}`);
        resources = await resp.json();
    } catch (err) {
        console.error("Failed to load materials:", err);
        return;
    }
    selectedCosts.addEventListener("change", () => {
        updateCosts();
    });

    console.log(currentValues);
    updateCosts();

    // Looks for checked radiobuttons, parses their values into ints using radix 10. Then gets the existing inputs that is loaded with the resource.
    function updateCosts() {

        const selectedRadio = selectedCosts.querySelector('input[name="cost"]:checked');
        if (!selectedRadio) return;

        const selectedValue = parseInt(selectedRadio.value, 10);

        // Get current selected values (from Razor-rendered fields)
        const existingSelects = resourceCostsContainer.querySelectorAll("select");
        const existingInputs = resourceCostsContainer.querySelectorAll("input[type='number']");

        console.log("Current values: ", currentValues);
        resourceCostsContainer.innerHTML = "";

        for (let i = 0; i < selectedValue; i++) {
            // Looks through current values, if there exist an object with the i index it will save it into saved, otherwise creat a new empty object.
            // This prevent the app from crashing if objects are undefined.
            const saved = currentValues[i] || {};
            const div = document.createElement("div");
            div.className = "form-group";
            div.innerHTML = `
          <div style="border: 1px solid grey; text-align: center; padding: 10px;">
            <label for="Material_ProductionCost_${i}__ResourceId">Resource ${i + 1}</label>
            <select name="Material.ProductionCost[${i}].ResourceId" class="form-control" style="background-color: #f8f9fa; text-align: center;">
              <option value="">-- Select Resource --</option>
              ${resources.map(r =>
                `<option value="${r.id}" ${saved.resourceId == r.id ? "selected" : ""}>${r.name}</option>`
            ).join("")}
            </select>

            <label for="Material_ProductionCost_${i}__Amount">Amount</label>
            <input type="number" name="Material.ProductionCost[${i}].Amount" class="form-control" value="${saved.amount || ""}" />
          </div>
          <br/>
        `;
            resourceCostsContainer.appendChild(div);
        }
    }

   
});

