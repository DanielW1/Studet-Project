var listElem = document.querySelectorAll(".c-triangle");
var infoElem = document.querySelectorAll(".c-content__list__info");
var map_panel = document.querySelectorAll('[jstcache="75"]');
var map_panel_warn = document.querySelector('[jstcache="54"]');
var buttonsLookOnMap = document.querySelectorAll('[button-marker="setMarker"]');
var buttonSetAllMarkers = document.querySelector('[button-marker="setAll"]')

    map_panel.forEach((element) => {
        element.style.display = "none";
    })

    if (map_panel_warn) {
        map_panel_warn.classList.add("u-hidden");
    }
    
//Za³o¿enie listenerów na strza³ki do rozwijania bloków informacji
listElem.forEach((item, index)=>{
    item.addEventListener("click", function(event){
        dropDownList(index);
    });
})

//Za³o¿enie Listenerów na przyciski "Poka¿ na mapie"
buttonsLookOnMap.forEach((element) => {
    element.addEventListener("click", function (event) {
        clearMapOnMarkers();
        const attribute = element.getAttribute("id");
        //Attrybut zapisany jest w postaci button_index, wyci¹gamy index, 
        //aby dostaæ siê do markera na mapie
        const index = parseInt(attribute.split("_")[1]);

        const marker = markers[index];
        highlightMarker(marker.marker);
    });
})

buttonSetAllMarkers.addEventListener("click", function (event) {
    setMapOnMarkers();
})

/* Usuwa wyró¿niaj¹ce marker ikony */
function clearMapOnMarkers() {
    markers.forEach((element) => {
        element.marker.setMap(null);
    })
}

function setMapOnMarkers() {
    markers.forEach((element) => {
        element.marker.setMap(map);
    })
}

/**
 * Funkcja pozwala zwijaæ i rozwijaæ odpowiednie bloki zawieraj¹ce informacje o parkingach 
 * na podstawie indeksu bloku i indeksu strza³ki rozwijaj¹cej
 * @param {any} index -- jest to index elementu w rozwijanej liœcie informacji o parkingach 
 * 
 */
function dropDownList(index){
    if(infoElem[index].classList.contains("u-hidden")){
        infoElem[index].classList.remove("u-hidden");
        listElem[index].classList.add("c-triangle-up");
    }else{
        infoElem[index].classList.add("u-hidden");
        listElem[index].classList.remove("c-triangle-up");
    }
}