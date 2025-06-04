document.addEventListener("DOMContentLoaded", async () => {
    const selectedCosts = document.getElementById("selectCosts");
    const materialCostsContainer = document.getElementById("materialCostsContainer");
    const selectedProperties = document.getElementById("selectProperties");
    const specialPropertiesContainer = document.getElementById("specialPropertiesContainer");
    const currentValues = initialProductionCosts || [];
    let materials = [];

    // Attempts to retrieve all materials from the database using the API endpoint.
    try {
        const resp = await fetch("https://localhost:7088/api/Material");
        if (!resp.ok) throw new Error(`HTTP ${resp.status}`);
        materials = await resp.json();
    } catch (err) {
        console.error("Failed to load materials:", err);
        return;
    }
    
    console.log(currentValues);
    updateCosts();

    updateProperties();

    selectedCosts.addEventListener("change", () => {
        updateCosts();
    });

    selectedProperties.addEventListener("change", () => {
        updateProperties();
    });


    // Looks for checked radiobuttons, parses their values into ints using radix 10. Then gets the existing inputs that is loaded with the material.
    function updateCosts() {

        const selectedRadio = selectedCosts.querySelector('input[name="cost"]:checked');
        if (!selectedRadio) return;

        const selectedValue = parseInt(selectedRadio.value, 10);

        // Get current selected values (from Razor-rendered fields)
        const existingSelects = materialCostsContainer.querySelectorAll("select");
        const existingInputs = materialCostsContainer.querySelectorAll("input[type='number']");

        console.log("Current values: ", currentValues);
        materialCostsContainer.innerHTML = "";

        for (let i = 0; i < selectedValue; i++) {
            // Looks through current values, if there exist an object with the i index it will save it into saved, otherwise creat a new empty object.
            // This prevent the app from crashing if objects are undefined.
            const saved = currentValues[i] || {};
            const div = document.createElement("div");
            div.className = "form-group";
            div.innerHTML = `
              <div style="border: 1px solid grey; text-align: center; padding: 10px;">
                <label for="Ammunition_ProductionCost_${i}__MaterialId">Material ${i + 1}</label>
                <select name="Ammunition.ProductionCost[${i}].MaterialId" class="form-control" style="background-color: #f8f9fa; text-align: center;">
                  <option value="">-- Select Material --</option>
                  ${materials.map(r =>
                    `<option value="${r.id}" ${saved.materialId == r.id ? "selected" : ""}>${r.name}</option>`
                ).join("")}
                </select>

                <label for="Ammunition_ProductionCost_${i}__Amount">Amount</label>
                <input type="number" name="Ammunition.ProductionCost[${i}].Amount" class="form-control" value="${saved.amount || ""}" />
              </div>
              <br/>
            `;
            materialCostsContainer.appendChild(div);
        }
    }

    // Pretty much the same as material costs but the list is populated from the cshtml.cs variable InitialSpecialProperties
   function updateProperties() {
    const selectedRadio = selectedProperties.querySelector('input[name="property"]:checked');
    if (!selectedRadio) return;

    const selectedValue = parseInt(selectedRadio.value, 10);
    specialPropertiesContainer.innerHTML = "";

    for (let i = 0; i < selectedValue; i++) {
        const savedValue = initialSpecialProperties[i] || "";

        const div = document.createElement("div");
        div.className = "form-group";
        div.innerHTML = `
            <label for="Ammunition_AmmoProperties_${i}">Ammunition Property ${i + 1}</label>
            <select name="Ammunition.AmmoProperties[${i}]" class="form-control">
                <option value="">-- Select Property --</option>
                ${specialProperties.map(p => 
                    `<option value="${p.value}" ${savedValue == p.value ? "selected" : ""}>${p.text}</option>`
                ).join("")}
            </select>
        `;
        specialPropertiesContainer.appendChild(div);
    }
}

});

