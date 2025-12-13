// Create
document.addEventListener("click", async (e) => {
  if (e.target.id === "createDiscountBtn") {
    console.log("Pressing Discount");
    const inputs = document.querySelectorAll(".discount-create");
    let isValid = true;
    const data = {};

    inputs.forEach((input) => {
      if (!input.value) {
        isValid = false;
        input.classList.add("border-red-500");
      } else {
        input.classList.remove("border-red-500");
        data[input.dataset.field] = input.value;
      }
    });

    if (!isValid) {
      document.getElementById("discount-error-message").textContent =
        "Please fill in all fields.";
      return;
    }

    const response = await fetch(`/Discount/Create`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data),
    });

    const html = await response.text();
    document.getElementById("main-content").innerHTML = html;
    ShowMessage("Discount created successfully");
  }
});
// Delete
document.addEventListener("click", async (e) => {
  if (e.target.classList.contains("delete-discount")) {
    const id = e.target.dataset.id;

    if (!confirm("Are you sure you want to delete this discount?")) return;

    const response = await fetch(`/Discount/Delete/${id}`, {
      method: "POST",
    });

    const html = await response.text();
    document.getElementById("main-content").innerHTML = html;
    ShowMessage("Discount deleted successfully");
  }
});
