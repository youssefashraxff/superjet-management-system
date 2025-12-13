// ADD TO CART
document.addEventListener("click", async (e) => {
  if (e.target.classList.contains("add-to-cart")) {
    const routeId = e.target.dataset.id;
    console.log("Route id:", routeId);
    const response = await fetch(`/Cart/Add/${routeId}`, {
      method: "POST",
    });

    if (response.status === 401) {
      window.location.href = "/User/Login";
      return;
    }

    const html = await response.text();
    document.getElementById("cart-container").innerHTML = html;
  }
});

// REMOVE CART ITEM
document.addEventListener("click", async (e) => {
  if (e.target.closest(".remove-cart-item")) {
    const btn = e.target.closest(".remove-cart-item");
    const itemId = btn.dataset.id;

    const response = await fetch(`/Cart/Remove/${itemId}`, {
      method: "POST",
    });

    if (response.status === 401) {
      window.location.href = "/User/Login";
      return;
    }

    const html = await response.text();
    document.getElementById("cart-container").innerHTML = html;
  }
});

// LOAD CART (on page load)
async function loadCart() {
  const response = await fetch("/Cart/Index");

  if (response.status === 401) {
    return;
  }
  const html = await response.text();
  document.getElementById("cart-container").innerHTML = html;
}
// APPLY DISCOUNT
document.addEventListener("click", async (e) => {
  if (e.target.classList.contains("apply-discount")) {
    const codeInput = document.getElementById("discountCode");
    const message = document.getElementById("discount-message");
    const successIcon = document.getElementById("discount-success-icon");

    const code = codeInput.value.trim();

    // reset UI state
    message.classList.add("hidden");
    message.textContent = "";
    successIcon?.classList.add("hidden");

    if (!code) {
      message.textContent = "Please enter a discount code";
      message.classList.remove("hidden");
      return;
    }

    const response = await fetch("/Cart/ApplyDiscount", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ code }),
    });

    if (!response.ok) {
      const text = await response.text();
      message.textContent = text;
      message.classList.remove("hidden");
      return;
    }

    // success → keep input value, show tick, update cart UI
    const html = await response.text();
    document.getElementById("cart-container").innerHTML = html;

    // re-show success tick after DOM replacement
    setTimeout(() => {
      const icon = document.getElementById("discount-success-icon");
      if (icon) icon.classList.remove("hidden");
    }, 0);
  }
});
document.addEventListener("click", async (e) => {
  if (e.target.classList.contains("checkout-tickets")) {
    const res = await fetch("/Ticket/Checkout", {
      method: "POST",
    });

    if (res.status === 401) {
      window.location.href = "/User/Login";
      return;
    }

    if (!res.ok) {
      alert(await res.text());
      return;
    }

    // Redirect to tickets page
    window.location.href = "/User/Profile";
  }
});
loadCart();
