var myGeoLocation; 
var userLocationMarker;

function geolocate() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var geolocation = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };
            var circle = new google.maps.Circle({
                center: geolocation,
                radius: position.coords.accuracy
            });
            autocomplete.setBounds(circle.getBounds());
        });
    }
}

function getMapLocation() {

    if (navigator.geolocation) {

        navigator.geolocation.getCurrentPosition(showMapPosition.bind(this));

    } else {

        map.innerHTML = "Mechanizm geolokacji jest niedostępny";

    }
    console.log("geo");

}

function showMapPosition(position) {

   const mapLatitude = position.coords.latitude;

   const mapLongitude = position.coords.longitude;

    myGeoLocation = { gpsLat: mapLatitude, gpsLng: mapLongitude, Name: 'Twoja lokalizacja' };
    if (!userLocationMarker) {
        userLocationMarker = createMarker(myGeoLocation);
        userLocationMarker.setMap(map);
    } else {
        userLocationMarker.setPosition({ lat: myGeoLocation.gpsLat, lng: myGeoLocation.gpsLng })
    }
    

    var latInput = document.getElementById("lat");
    var lngInput = document.getElementById("lng");
    if (latInput && lngInput) {
        latInput.value = myGeoLocation.gpsLat;
        lngInput.value = myGeoLocation.gpsLng;

    }
}

