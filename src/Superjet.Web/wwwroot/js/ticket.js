document.querySelectorAll(".view-ticket-details").forEach((btn) => {
  btn.addEventListener("click", async () => {
    const id = btn.dataset.id;

    const res = await fetch(`/Ticket/Details/${id}`);
    const html = await res.text();

    // Load into admin main content area
    document.getElementById("main-content").innerHTML = html;
  });
});
