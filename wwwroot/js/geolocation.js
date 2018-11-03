var myGeoLocation; 

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

        navigator.geolocation.getCurrentPosition(showMapPosition);

    } else {

        map.innerHTML = "Mechanizm geolokacji jest niedostępny";

    }

}

function getGeoLoaction() {
    return myGeoLocation;
}

function showMapPosition(position) {

   const mapLatitude = position.coords.latitude;

   const mapLongitude = position.coords.longitude;

    myGeoLocation = { gpsLat: mapLatitude, gpsLng: mapLongitude, Name: 'Twoja lokalizacja' };

    var marker = createMarker(myLatlng);
    marker.setIcon('@Url.Content("~/images/circle.png")');
    marker.setMap(map);
}
