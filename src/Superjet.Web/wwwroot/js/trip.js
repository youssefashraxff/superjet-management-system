let cart = [];

document.addEventListener("click", (e) => {
  if (!e.target.classList.contains("select-trip")) return;

  const model = e.target.dataset.model;
  const origin = e.target.dataset.origin;
  const destination = e.target.dataset.destination;
  const departure = new Date(e.target.dataset.departure);
  const arrival = new Date(e.target.dataset.arrival);
  const price = parseFloat(e.target.dataset.price);

  cart.push({
    model,
    origin,
    destination,
    departure,
    arrival,
    price,
  });

  renderCart();
});

function renderCart() {
  const container = document.getElementById("ticket-summary");
  const totalEl = document.getElementById("ticket-total");

  if (!cart.length) {
    container.innerHTML = `<p class="text-gray-400 text-sm">No tickets selected yet.</p>`;
    totalEl.innerHTML = `<span>Total</span><span>0 EGP</span>`;
    return;
  }

  let total = 0;

  container.innerHTML = cart
    .map((t) => {
      total += t.price;

      return `
      <div class="flex flex-col gap-1 p-4 border border-gray-200 rounded-lg mb-3">
        <span class="px-2 py-1 text-xs font-medium text-gray-800 bg-orange-400 rounded-tr-md rounded-bl-md w-fit">
          ${t.model} Bus
        </span>

        <div class="flex items-center gap-2 font-semibold text-gray-700 text-md">
          <p>${t.departure.toLocaleTimeString([], {
            hour: "2-digit",
            minute: "2-digit",
          })}</p>
          <div class="flex items-center">
            <div class="w-1 h-1 bg-gray-400 rounded-full"></div>
            <div class="w-10 h-px bg-gray-400"></div>
            <div class="w-1 h-1 border border-gray-400 rounded-full"></div>
          </div>
          <p>${t.arrival.toLocaleTimeString([], {
            hour: "2-digit",
            minute: "2-digit",
          })}</p>
        </div>

        <p class="text-xs text-gray-400">${t.departure.toDateString()}</p>

        <p class="text-xs text-gray-600">
          ${t.origin} <i class="fas fa-arrow-right"></i> ${t.destination}
        </p>

        <p class="flex justify-between">
          <span>Ticket</span> <span>${t.price} EGP</span>
        </p>
      </div>`;
    })
    .join("");

  totalEl.innerHTML = `<span>Total</span><span>${total.toFixed(2)} EGP</span>`;
}
document.addEventListener("click", async (e) => {
  if (e.target.classList.contains("checkout-tickets")) {
    if (localStorage.getItem("Token")) {
    } else {
    }
  }
});
