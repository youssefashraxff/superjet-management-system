let searchForTrips = () => {
  let destination = document.getElementById("DestSelect").value;
  let origin = document.getElementById("OriginSelect").value;
  let date = document.getElementById("DateSelect").value;

  window.location.href = `/Trips/Search?origin=${origin}&destination=${destination}&date=${date}`;
};
let FilterTrips = () => {
  console.log("Filterrrr");
  const models = [...document.querySelectorAll(".model-checkbox:checked")].map(
    (c) => c.value
  );

  const minPrice = document.getElementById("minPrice").value;
  const maxPrice = document.getElementById("maxPrice").value;

  const depTimes = [...document.querySelectorAll(".time-checkbox:checked")].map(
    (c) => c.value
  );
  console.log(maxPrice);
  console.log(minPrice);
  let params = new URLSearchParams();

  models.forEach((m) => params.append("models", m));
  depTimes.forEach((t) => params.append("DepartureTimes", t));

  params.append("minPrice", minPrice);
  params.append("maxPrice", maxPrice);

  let url = `/Trips/Filter?${params.toString()}`;

  fetch(url)
    .then((response) => response.text())
    .then((html) => {
      document.getElementById("tripsContainer").innerHTML = html;
    });
};

document.querySelectorAll(".sidebar-link").forEach((link) => {
  link.addEventListener("click", async (e) => {
    e.preventDefault();
    console.log("Pressed");
    const url = link.dataset.url;

    const response = await fetch(url);
    const html = await response.text();

    // Replace only the main content
    document.getElementById("main-content").innerHTML = html;
  });
});

let ShowMessage = (message) => {
  const toast = document.getElementById("toast");
  toast.textContent = message;

  toast.classList.remove("opacity-0", "hidden");

  setTimeout(() => {
    toast.classList.add("opacity-0", "hidden");
  }, 2000);
};
