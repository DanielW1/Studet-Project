const buttonRoad = document.querySelectorAll('[button-marker="road"]');
const startParking = document.getElementById("startParking");
const buttonGenerateTransitRoad = document.getElementById("designate");
const autocompleteEnd = document.getElementById("autocompleteEnd");
const freePlacesField = document.querySelectorAll("[data-freeplaces='freePlaces']");

buttonRoad.forEach((item, index) => {
    item.addEventListener("click", function () {
        drawRoadToSelectedParking(index);
        insertDataToInputSearchField(index);
    })
})

freePlacesField.forEach((item) => {
    if (item.innerHTML < 10) {
        item.style.color = "red";
    } else {
        item.style.color = "green";
    }

})

if (buttonGenerateTransitRoad) {
    buttonGenerateTransitRoad.addEventListener("click", function () {
        if (autocompleteEnd.value && startParking.value) {
            const place = autocomplete.getPlace();
            const selectedMarker = markers.find(x => x.name == startParking.value);
            const originLat = selectedMarker.marker.position.lat();
            const originLng = selectedMarker.marker.position.lng();
            //podajemy czas w którym chcielibysmy przesiąść się na komunikację
            //jest to czas od chwili obecnej + szacowany czas podróży w sekundach pomnożony przez 1000, gdyż liczymy w milisekundach
            //od 1 stycznia 1970 roku
            const time = new Date(Date.now()+parseInt(selectedMarker.duration)*1000);

            calculateAndDisplayRouteTransitTime.
                call(directionServiceTransit,
                { lat: originLat, lng: originLng }, { lat: place.geometry.location.lat(), lng: place.geometry.location.lng() }, time)

        } else {
            alert("Uzupełnij wszystkie pola");
        }
    })
}
function drawRoadToSelectedParking(index) {
    const lat = markers[index].marker.position.lat();
    const lng = markers[index].marker.position.lng();
    getMapLocation();
    calculateAndDisplayRouteDrive.call(directionServiceDrive, { lat: myGeoLocation.gpsLat, lng: myGeoLocation.gpsLng }, { lat: lat, lng: lng });
}

function insertDataToInputSearchField(index) {
    startParking.value = markers[index].name;
}