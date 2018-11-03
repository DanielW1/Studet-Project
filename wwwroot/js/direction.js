var directionsService;
var directionsDisplay;


function directionsServiceAdd(mapPanelclass) {
     directionsService = new google.maps.DirectionsService;
     directionsDisplay = new google.maps.DirectionsRenderer;

    directionsDisplay.setMap(map);

    if (mapPanelclass) {
        directionsDisplay.setPanel(document.querySelector(mapPanelclass));
    }

}

function calculateAndDisplayRoute( origin, destination, mode, date) {
    directionsService.route({
        origin: origin,
        destination: destination,
        travelMode: google.maps.TravelMode[mode],
        provideRouteAlternatives: true,
        transitOptions: {
            departureTime: date
        }
    }, function (response, status) {
        if (status === 'OK') {
            directionsDisplay.setDirections(response);
        } else {
            window.alert('Żądanie nie powiodło się: ' + status);
        }
    });
}

function calculateAndDisplayRouteDrive( origin, destination) {
    calculateAndDisplayRoute( origin, destination, 'DRIVING', new Date());
}

function calculateAndDisplayRouteTransit(origin, destination ) {
    calculateAndDisplayRoute(origin, destination, 'TRANSIT', new Date());
}

function calculateAndDisplayRouteTransitTime(origin, destination, time) {
    calculateAndDisplayRoute(origin, destination, 'TRANSIT', time);
}