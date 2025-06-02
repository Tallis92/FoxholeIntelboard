document.addEventListener("DOMContentLoaded", async () => {
    const selectedCosts = document.getElementById("selectCosts");
    const resourceCostsContainer = document.getElementById("resourceCostsContainer");
    let resources = [];

    // Attempts to retrieve all resources from the database using the API endpoint.
    try {
        const resp = await fetch("https://localhost:7088/api/Resource");
        if (!resp.ok) throw new Error(`HTTP ${resp.status}`);
        resources = await resp.json();
    } catch (err) {
        console.error("Failed to load resources:", err);
        return;
    }

    // Looks for any changes in the radio buttons, then tries to parse the value as int with radix 10 to make sure that the program only reads
    // it as an int. Then creates a new dropdown item for each value the user has selected and binds it to the Resource.ProductionCost property.
    selectedCosts.addEventListener("change", () => {
        const selectedRadio = selectedCosts.querySelector('input[name="cost"]:checked');
        if (!selectedRadio) return;

        const selectedValue = parseInt(selectedRadio.value, 10);
        resourceCostsContainer.innerHTML = "";

        for (let i = 0; i < selectedValue; i++) {
            const div = document.createElement("div");
            div.className = "form-group";
            div.innerHTML = `
              <div style="border: 1px solid grey; text-align: center; padding: 10px;">
                <label for="Material_ProductionCost_${i}__ResourceId">
                  Resource ${i + 1}
                </label>
                <select
                  name="Material.ProductionCost[${i}].ResourceId"
                  class="form-control"
                  style="background-color: #f8f9fa; text-align: center;"
                >
                  <option value="">-- Select Resource --</option>
                  ${resources.map(r => `<option value="${r.id}">${r.name}</option>`).join("")}
                </select>

                <label for="Material_ProductionCost_${i}__Amount">
                  Amount
                </label>
                <input
                  type="number"
                  name="Material.ProductionCost[${i}].Amount"
                  class="form-control"
                />
              </div>
              <br/>
            `;
            resourceCostsContainer.appendChild(div);
        }
    });
});