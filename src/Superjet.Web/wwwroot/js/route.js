// Delete Route
document.addEventListener("click", async (e) => {
  if (e.target.classList.contains("delete-route")) {
    const id = e.target.dataset.id;
    console.log("Delete");
    if (!confirm("Are you sure you want to delete this route?")) return;

    const response = await fetch(`/Route/Delete/${id}`, {
      method: "DELETE",
    });

    const html = await response.text();
    document.getElementById("main-content").innerHTML = html;
    ShowMessage("Route deleted successfully");
  }
});
// Edit Route
document.addEventListener("click", async (e) => {
  if (e.target.classList.contains("save-route")) {
    const id = e.target.dataset.id;

    // select the row
    const row = e.target.closest("tr");

    // read inputs in order
    const inputs = row.querySelectorAll("input");

    const data = {
      Id: id,
      Origin: inputs[0].value,
      Destination: inputs[1].value,
      DepartureTime: inputs[2].value,
      ArrivalTime: inputs[3].value,
      Price: inputs[4].value,
    };

    // send update request
    const response = await fetch(`/Route/Edit`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data),
    });

    const html = await response.text();

    // replace table content
    document.getElementById("main-content").innerHTML = html;
    ShowMessage("Route edited successfully");
  }
});
// Add Route
// OPEN MODAL
document.addEventListener("click", (e) => {
  if (e.target.classList.contains("add-route")) {
    document.getElementById("addRouteModal").classList.remove("hidden");
  }
});

// CLOSE MODAL
document.addEventListener("click", (e) => {
  if (e.target.id === "closeAddModal") {
    document.getElementById("addRouteModal").classList.add("hidden");
  }
});

// SUBMIT CREATE
document.addEventListener("click", async (e) => {
  if (e.target.id === "submitAddRoute") {
    const inputs = document.querySelectorAll(".route-modal-input");
    let isValid = true;
    const data = {};

    inputs.forEach((input) => {
      if (!input.value) {
        isValid = false;
        input.classList.add("border-red-500");
      } else {
        input.classList.remove("border-red-500");
      }
      data[input.dataset.field] = input.value;
    });
    console.log({ data });
    if (!isValid) {
      document.getElementById("error-message").textContent =
        "Please fill in all fields";
      return;
    }

    document.getElementById("addRouteModal").classList.add("hidden");
    console.log({ data });
    const response = await fetch(`/Route/Create`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data),
    });

    const html = await response.text();
    document.getElementById("main-content").innerHTML = html;
    ShowMessage("Route added successfully");
  }
});

//  View Ticket
document.addEventListener("click", async (e) => {
  if (e.target.classList.contains("view-tickets")) {
    const routeId = e.target.dataset.routeid;

    const res = await fetch(`/Ticket/Index?routeId=${routeId}`);
    const html = await res.text();

    document.getElementById("main-content").innerHTML = html;
  }
});
