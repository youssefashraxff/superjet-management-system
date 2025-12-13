document.addEventListener("click", async (e) => {
  if (e.target.classList.contains("cancel-booking")) {
    console.log("Cancelllllllll");
    const ticketId = e.target.dataset.id;

    if (!confirm("Are you sure you want to cancel this ticket?")) return;

    const response = await fetch(`/Ticket/Cancel/${ticketId}`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
    });

    if (!response.ok) {
      alert("Failed to cancel ticket");
      return;
    }

    window.location.href = "/User/Profile";
    // Controller returns updated Details partial
    // const html = await response.text();
    // document.getElementById("main-content").innerHTML = html;
  }
});
