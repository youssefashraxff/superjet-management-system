document.querySelectorAll(".view-ticket-details").forEach((btn) => {
  btn.addEventListener("click", async () => {
    const id = btn.dataset.id;

    const res = await fetch(`/Ticket/Details/${id}`);
    const html = await res.text();

    // Load into admin main content area
    document.getElementById("main-content").innerHTML = html;
  });
});

document.addEventListener("click", async (e) => {
  if (e.target.classList.contains("view-ticket-details")) {
    const id = e.target.dataset.id;

    const response = await fetch(`/Ticket/Details/${id}`);
    const html = await response.text();

    document.getElementById("main-content").innerHTML = html;
  }
});

document.addEventListener("click", async (e) => {
  if (e.target.classList.contains("load-route-tickets")) {
    const routeId = e.target.dataset.routeId;

    const response = await fetch(`/Ticket/Index?routeId=${routeId}`);
    const html = await response.text();

    document.getElementById("main-content").innerHTML = html;
  }
});
