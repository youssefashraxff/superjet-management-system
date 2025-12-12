// =========================
// LOAD VIA SIDEBAR
// =========================
document.querySelectorAll(".sidebar-link").forEach((link) => {
  link.addEventListener("click", async (e) => {
    e.preventDefault();
    const url = link.dataset.url;

    const response = await fetch(url);
    const html = await response.text();

    document.getElementById("main-content").innerHTML = html;
  });
});

// =========================
// DELETE BUS
// =========================
document.addEventListener("click", async (e) => {
  if (e.target.classList.contains("delete-bus")) {
    const id = e.target.dataset.id;

    if (!confirm("Are you sure you want to delete this bus?")) return;

    const response = await fetch(`/Bus/Delete/${id}`, {
      method: "POST",
    });
    if (response.status === 400) {
      ShowMessage("Unable to delete bus. It is assigned to routes.");
      return;
    }
    const html = await response.text();
    document.getElementById("main-content").innerHTML = html;
    ShowMessage("Bus deleted successfully");
  }
});

// =========================
// EDIT (SAVE) BUS
// =========================
document.addEventListener("click", async (e) => {
  if (e.target.classList.contains("save-bus")) {
    const id = e.target.dataset.id;

    const row = e.target.closest("tr");
    const inputs = row.querySelectorAll(".bus-input");

    const data = { Id: id };

    inputs.forEach((input) => {
      data[input.dataset.field] = input.value;
    });

    const response = await fetch(`/Bus/Edit`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data),
    });

    const html = await response.text();
    document.getElementById("main-content").innerHTML = html;

    ShowMessage("Bus updated successfully");
  }
});

// =========================
// OPEN ADD BUS MODAL
// =========================
document.addEventListener("click", (e) => {
  if (e.target.classList.contains("add-bus")) {
    document.getElementById("addBusModal").classList.remove("hidden");
  }
});

// =========================
// CLOSE ADD BUS MODAL
// =========================
document.addEventListener("click", (e) => {
  if (e.target.id === "closeAddBusModal") {
    document.getElementById("addBusModal").classList.add("hidden");
  }
});

// =========================
// SUBMIT ADD BUS
// =========================
document.addEventListener("click", async (e) => {
  if (e.target.id === "submitAddBus") {
    const inputs = document.querySelectorAll(".bus-modal-input");
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
      console.log("Bus Data", { data });
    });

    if (!isValid) {
      document.getElementById("bus-error-message").textContent =
        "Please fill in all fields";
      return;
    }

    document.getElementById("addBusModal").classList.add("hidden");
    console.log("Bus Data", { data });
    const response = await fetch(`/Bus/Create`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data),
    });

    const html = await response.text();
    document.getElementById("main-content").innerHTML = html;

    ShowMessage("Bus added successfully");
  }
});
