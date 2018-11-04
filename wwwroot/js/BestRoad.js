const buttonRoad = document.querySelectorAll('[button-marker="road"]');

buttonRoad.forEach((item, index) => {
    item.addEventListener("click", function () {
        drawRoadToSelectedParking(index);  
    })
})
function drawRoadToSelectedParking(index) {
    const lat = markers[index].marker.position.lat();
    const lng = markers[index].marker.position.lng();
    getMapLocation();
    calculateAndDisplayRouteDrive({ lat: myGeoLocation.gpsLat, lng: myGeoLocation.gpsLng }, {lat:lat, lng:lng});
}