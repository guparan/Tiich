function DefaultPlacement(map) {
    var x = 50;
    var y = 50;
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
            function (pos) {

                //center
                x = pos.coords.latitude;
                y = pos.coords.longitude;
                var me = new google.maps.LatLng(pos.coords.latitude, pos.coords.longitude);
                mapOptions = {
                    center: { lat: x, lng: y },
                    zoom: 17
                };

                //Place
                var marker = new google.maps.Marker({
                    position: new google.maps.LatLng(x, y),
                    map: map,
                    title: 'Vous êtes ici'
                });
            }
            );
    }
    else {
        alert("Pas de géoloc");
    }
}

function SearchBox(map)
{

    // Create the search box and link it to the UI element.
    var input = /** @type {HTMLInputElement} */(
        document.getElementById('location'));
    map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
    alert(google.maps.places);
    var searchBox = new google.maps.places.SearchBox(
      /** @type {HTMLInputElement} */(input));

    google.maps.event.addListener(searchBox, 'places_changed', function() {
        var places = searchBox.getPlaces();

        if (places.length == 0) {
            return;
        }
        for (var i = 0, marker; marker = markers[i]; i++) {
            marker.setMap(null);
        }
    });

}

function initialize() {
    /*var mapOptions = {
                    center: { lat: 0, lng: 0 },
                    zoom: 17
                };
    var map = new google.maps.Map(document.getElementById('map'), mapOptions);

    DefaultPlacement(map);
    SearchBox(map);*/


    var markers = [];
    var map = new google.maps.Map(document.getElementById('map'), {
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });

    var defaultBounds = new google.maps.LatLngBounds(
        new google.maps.LatLng(-33.8902, 151.1759),
        new google.maps.LatLng(-33.8474, 151.2631));
    map.fitBounds(defaultBounds);

    // Create the search box and link it to the UI element.
    var input = /** @type {HTMLInputElement} */(
        document.getElementById('location'));
    map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

    var searchBox = new google.maps.places.SearchBox(
      /** @type {HTMLInputElement} */(input));

    // [START region_getplaces]
    // Listen for the event fired when the user selects an item from the
    // pick list. Retrieve the matching places for that item.
    google.maps.event.addListener(searchBox, 'places_changed', function () {
        var places = searchBox.getPlaces();

        if (places.length == 0) {
            return;
        }
        for (var i = 0, marker; marker = markers[i]; i++) {
            marker.setMap(null);
        }

        // For each place, get the icon, place name, and location.
        markers = [];
        var bounds = new google.maps.LatLngBounds();
        for (var i = 0, place; place = places[i]; i++) {
            var image = {
                url: place.icon,
                size: new google.maps.Size(71, 71),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(17, 34),
                scaledSize: new google.maps.Size(25, 25)
            };

            // Create a marker for each place.
            var marker = new google.maps.Marker({
                map: map,
                icon: image,
                title: place.name,
                position: place.geometry.location
            });

            markers.push(marker);

            bounds.extend(place.geometry.location);
        }

        map.fitBounds(bounds);
    });
    // [END region_getplaces]

    // Bias the SearchBox results towards places that are within the bounds of the
    // current map's viewport.
    google.maps.event.addListener(map, 'bounds_changed', function () {
        var bounds = map.getBounds();
        searchBox.setBounds(bounds);
    });
   
}

google.maps.event.addDomListener(window, 'load', initialize);
//google.maps.event.addDomListener(window, 'load', initialize);
