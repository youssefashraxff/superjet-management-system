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

// ASSIGN BUS
let selectedRouteId = null;
document.addEventListener("click", async (e) => {
  // OPEN ASSIGN BUS MODAL (use closest to handle inner spans)
  const assignEl = e.target.closest(".assign-bus");
  if (assignEl) {
    selectedRouteId = assignEl.dataset.routeid;

    const modal = document.getElementById("assignBusModal");
    const select = document.getElementById("busSelect");
    const errorEl = document.getElementById("assignBusError");

    errorEl.classList.add("hidden");
    errorEl.textContent = "";
    select.innerHTML = `<option value="">Loading...</option>`;

    modal.classList.remove("hidden");

    const res = await fetch("/Bus/GetAvailable");
    const buses = await res.json();

    select.innerHTML = `<option value="" disabled selected>Select Bus</option>`;
    buses.forEach((b) => {
      select.innerHTML += `
        <option value="${b.id}">
          ${b.busNo} - ${b.model}
        </option>`;
    });
    return;
  }

  // CLOSE MODAL
  if (e.target.id === "closeAssignBus") {
    document.getElementById("assignBusModal").classList.add("hidden");
    return;
  }

  // SAVE ASSIGNMENT
  if (e.target.id === "saveAssignBus") {
    const busId = document.getElementById("busSelect").value;
    const errorEl = document.getElementById("assignBusError");

    if (!busId) {
      errorEl.textContent = "Please select a bus";
      errorEl.classList.remove("hidden");
      return;
    }

    const res = await fetch("/Route/AssignBus", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        routeId: selectedRouteId,
        busId: busId,
      }),
    });

    if (!res.ok) {
      const msg = await res.text();
      errorEl.textContent = msg;
      errorEl.classList.remove("hidden");
      return;
    }

    document.getElementById("assignBusModal").classList.add("hidden");
    location.reload();
  }
});
