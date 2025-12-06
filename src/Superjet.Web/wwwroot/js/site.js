let searchForTrips = () => {
  let destination = document.getElementById("DestSelect").value;
  let origin = document.getElementById("OriginSelect").value;
  let date = document.getElementById("DateSelect").value;

  window.location.href = `/Trips/Search?origin=${origin}&destination=${destination}&date=${date}`;
};
