document.addEventListener("DOMContentLoaded", async () => {
    const selectedCosts = document.getElementById("selectCosts");
    const materialCostsContainer = document.getElementById("materialCostsContainer");
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

    // Looks for any changes in the radio buttons, then tries to parse the value as int with radix 10 to make sure that the program only reads
    // it as an int. Then creates a new dropdown item for each value the user has selected and binds it to the Ammunitions.ProductionCost property.
    selectedCosts.addEventListener("change", () => {
        const selectedRadio = selectedCosts.querySelector('input[name="cost"]:checked');
        if (!selectedRadio) return;

        const selectedValue = parseInt(selectedRadio.value, 10);
        materialCostsContainer.innerHTML = "";

        for (let i = 0; i < selectedValue; i++) {
            const div = document.createElement("div");
            div.className = "form-group";
            div.innerHTML = `
              <div style="border: 1px solid grey; text-align: center; padding: 10px;">
                <label for="Ammunition_ProductionCost_${i}__MaterialId">
                  Material ${i + 1}
                </label>
                <select
                  name="Ammunition.ProductionCost[${i}].MaterialId"
                  class="form-control"
                  style="background-color: #f8f9fa; text-align: center;"
                >
                  <option value="">-- Select Material --</option>
                  ${materials.map(r => `<option value="${r.id}">${r.name}</option>`).join("")}
                </select>

                <label for="Ammunition_ProductionCost_${i}__Amount">
                  Amount
                </label>
                <input
                  type="number"
                  name="Ammunition.ProductionCost[${i}].Amount"
                  class="form-control"
                />
              </div>
              <br/>
            `;
            materialCostsContainer.appendChild(div);
        }
    });

    // Pretty much the same as material costs but the list is populated from the cshtml.cs variable WeaponPropertiesOptions
    const selectedProperties = document.getElementById("selectProperties");
    const specialPropertiesContainer = document.getElementById("specialPropertiesContainer");

    selectedProperties.addEventListener("change", () => {
        const selectedRadio = selectedProperties.querySelector('input[name="property"]:checked');
        if (!selectedRadio) return;

        const selectedValue = parseInt(selectedRadio.value, 10);
        specialPropertiesContainer.innerHTML = "";

        for (let i = 0; i < selectedValue; i++) {
            const div = document.createElement("div");
            div.className = "form-group";
            div.innerHTML = `
                            <label for="Ammunition_AmmoProperties_${i}">Ammunition Property ${i + 1}</label>
                            <select name="Ammunition.AmmoProperties[${i}]" class="form-control">
                            <option value="">-- Select Property --</option>
                            ${specialProperties.map(p => `<option value="${p.value}">${p.text}</option>`).join("")}
                        </select>
                    `;
            specialPropertiesContainer.appendChild(div);
        }
    });
});