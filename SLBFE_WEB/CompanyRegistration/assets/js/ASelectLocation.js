// Note: This example requires that you consent to location sharing when
// prompted by your browser. If you see the error "The Geolocation service
// failed.", it means you probably did not give permission for the browser to
// locate you.
let map, infoWindow;

let center;

function initMap() {
  map = new google.maps.Map(document.getElementById("map"), {
    center: { lat: -34.397, lng: 150.644 },
    zoom: 18,
  });

  

    // Try HTML5 geolocation.
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(
        (position) => {
          const pos = {
            lat: position.coords.latitude,
            lng: position.coords.longitude,
          };

          var marker = new google.maps.Marker({
            position: pos,
            map: map,
          });

          map.addListener("click", (e) => {
            placeMarkerAndPanTo(e.latLng, map, marker);
          });

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
  }

function handleLocationError(browserHasGeolocation, pos) {

}

window.initMap = initMap;