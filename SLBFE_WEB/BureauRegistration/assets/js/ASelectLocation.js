// Note: This example requires that you consent to location sharing when
// prompted by your browser. If you see the error "The Geolocation service
// failed.", it means you probably did not give permission for the browser to
// locate you.
let map, infoWindow, marker;

let center;

function initMap() {
  map = new google.maps.Map(document.getElementById("map"), {
    center: { lat: -34.397, lng: 150.644 },
    zoom: 18,
  });

  marker = new google.maps.Marker({
    position: center,
    map: map,
  });

  map.addListener("click", (e) => {
    placeMarkerAndPanTo(e.latLng, map, marker);
  });

    // Try HTML5 geolocation.
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(
        (position) => {
          const pos = {
            lat: position.coords.latitude,
            lng: position.coords.longitude,
          };
          
          document.getElementById("lat").innerHTML = pos.lat;
          document.getElementById("lng").innerHTML = pos.lng;
          console.log("lat: " + pos.lat);
          console.log("lng: " + pos.lng);
          marker.setPosition(pos);

          map.setCenter(pos);
        },
        () => {
          handleLocationError(true, map.getCenter());
        }
      );
    } else {
      // Browser doesn't support Geolocation
      handleLocationError(false, map.getCenter());
    }
  };

  function placeMarkerAndPanTo(latLng, map, marker) {
    marker.setPosition(latLng);
    var ltln = latLng.toJSON()
    console.log(ltln);
    console.log("lat: " + ltln.lat);
    console.log("lng: " + ltln.lng);
    map.panTo(latLng);
    document.getElementById("lat").value = ltln.lat;
    document.getElementById("lng").value = ltln.lng;
  }

function handleLocationError(browserHasGeolocation, pos) {
  console.log("location access blocked by the browser");
}

window.initMap = initMap;