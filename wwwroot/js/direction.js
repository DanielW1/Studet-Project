
function DirectionService() {
    this.directionsService = new google.maps.DirectionsService;
    this.directionsDisplay = new google.maps.DirectionsRenderer;
    this.directionsServiceAdd = directionsServiceAdd;
    this.calculateAndDisplayRoute = calculateAndDisplayRoute;
}


function directionsServiceAdd(mapPanelclass) {

    this.directionsDisplay.setMap(map);

    if (mapPanelclass) {
        this.directionsDisplay.setPanel(document.querySelector(mapPanelclass));
    }

}

function calculateAndDisplayRoute( origin, destination, mode, date) {
    this.directionsService.route({
        origin: origin,
        destination: destination,
        travelMode: google.maps.TravelMode[mode],
        provideRouteAlternatives: true,
        transitOptions: {
            departureTime: date
        }
    }, (response, status)=> {
        if (status === 'OK') {
            this.directionsDisplay.setDirections(response);
        } else {
            window.alert('Żądanie nie powiodło się: ' + status);
        }
    });
}

function calculateAndDisplayRouteDrive( origin, destination) {
    this.calculateAndDisplayRoute( origin, destination, 'DRIVING', new Date());
}

function calculateAndDisplayRouteTransit(origin, destination ) {
    this.calculateAndDisplayRoute(origin, destination, 'TRANSIT', new Date());
}

function calculateAndDisplayRouteTransitTime(origin, destination, time) {
    this.calculateAndDisplayRoute(origin, destination, 'TRANSIT', time);
}