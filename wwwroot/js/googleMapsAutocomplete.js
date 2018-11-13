var placeSearch, autocomplete;

/*
 * Funkcja dodaje funkcjonalność API Google Autocomplete do elementu którego ID wskazujemy w parametrze funkcji
 * Domyślnie kręg poszukiwań zawężony do Warszawy - 'defaultBounds' możliwe rozszerzenie funkcji o podanie punktu zawężenia
 * */
function initAutocomplete(elemName) {

    var defaultBounds = new google.maps.LatLngBounds(
        new google.maps.LatLng(52.229, 21.011));

    autocomplete = new google.maps.places.Autocomplete(
        (document.getElementById(elemName)),
        {
            types: ['geocode'],
            bounds: defaultBounds

        });

    autocomplete.addListener('place_changed', function () {
        var places = autocomplete.getPlace();

        if (places.length == 0) {
            return;
        }

    })
}

