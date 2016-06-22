/*The Route Optimising SideKick was created as a 4th year final project for Gavin Gaughran x12107077 National College of Ireland 16.05.16

Code used throughout was written by Gavin Gaughran using, references and modified snippets from to the following websites:
    StackOverflow(General coding practice and troubleshooting)
    Code.MSDN(Algorithm mathematical equation)
    Damien Dennehy(Haversine information)
    Rubicite(PMX Crossover information)
    JSFiddle(JQuery and JavaScript functionality)
    W3Schools(CSS and design)
*/
//Globally declaring map elements
var map;
var geoCoder;
var directionsDisplay = new google.maps.DirectionsRenderer;
var directionsService = new google.maps.DirectionsService;

//Details for marker-labels
var labels = 'ABCDEFGHIJKLMONPQRSTUVWXYZ';
var labelIndex = 0;

window.initMap = function () {
    geocoder = new google.maps.Geocoder();

    directionsDisplay = new google.maps.DirectionsRenderer({
            suppressMarkers: false
    });
    
    //Create new map within the 'map' div with default lat and long and a default zoom of 7.
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: 53.3498, lng: -6.2603 },
        zoom: 7
    });

    //Find current lat and long location and set this as default starting position
    if (navigator.geolocation) {
        browserSupportFlag = true;
        navigator.geolocation.getCurrentPosition(function (position) {
            initialLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
            map.setCenter(initialLocation);
        }, function () {
            handleNoGeolocation(browserSupportFlag);
        });
    }
        //  The else statement is for browsers not supporting GeoLocation, current browser support for the W3C Geolocation API as of 16-05-16
        //  iPhone 3.0 +, Android 2.0 +, Firefox 3.5, Safari 5.0, Chrome 5.0, Internet Explorer 9.0+
    else {
        browserSupportFlag = false;
        handleNoGeolocation(browserSupportFlag);
    }

    directionsDisplay.setMap(map);
}

function calcRoute(coordsConvertedToString) {
    //Determines if the display will be for DRIVING, WALKING or BICYCLING
    var selectedMode = document.getElementById('mode').value;
    
    var amountOfFields = numFldVariable.value;

    var lat = new Array();
    var lng = new Array();
    var coords = []
    var splitAdds = new Array();

    //Create new array from split content of coordsConvertedToString string
    splitAdds = coordsConvertedToString.split(',');

    for (var i = 0; i < splitAdds.length-1; i++) {
        coords = splitAdds[i].split('#');
        if (coords.length == 2)
        {
            lat.push(coords[0]);
            lng.push(coords[1]);
        }
    }
    
    var waypts = [];
    //Loop for adding waypoints to map
    for (var i = 0; i < amountOfFields; i++) {
        stop = new google.maps.LatLng(lat[i], lng[i])
        waypts.push({ location: stop, stopover: true });
        //createMarker(stop);
    }

    //Start and end point
    var start = new google.maps.LatLng(lat[0], lng[0]);
    var end = new google.maps.LatLng(lat[0], lng[0]);

    //Set div for displaying trip details
    directionsDisplay.setPanel(document.getElementById('mapside'));

    var request = {
        origin: start,
        destination: end,
        waypoints: waypts,
        optimizeWaypoints: false,
        travelMode: google.maps.TravelMode[selectedMode]
    };

    directionsService.route(request, function (response, status) {
        if (status == google.maps.DirectionsStatus.OK) {
            directionsDisplay.setDirections(response);
            //var route = response.routes[0];
        }
    });
}

/*function createMarker(latlng) {

    var marker = new google.maps.Marker({
        position: latlng,
        //label: labels[labelIndex++ % labels.length],
        map: map
    });
}*/

$(document).ready(function () {
    //Monitor the button (id:verifyButton) for a click event
    $('#verifyButton').click(function (e) {
        //Array to store amount of boxes that need to be filled
        var array = [];

        var arrayOfDestinationNameValues = [];
        var arrayOfLatLngValues = [];

        //var arrayOfDestinationNameValues = [];
        var isValid = true;
        var count = 0;
        //Look for any input textboxes inside the mapInputSection div
        $('#mapInputSection input[type="text"]').each(function () {
            count++;
            //If box is empty get the attribute id and store it to array
            if ($.trim($(this).val()) == '') {
                isValid = false;
                var id = $(this).attr('id');
                array.push(id);
                //If empty change CSS to make Red as not filled in yet
                $(this).css({
                    "border": "2px solid red", "background": "#FFCECE"
                });
            } else {
                //If details are in then store value of textbox into array
                var valueOfTextbox = $(this).val();
                arrayOfDestinationNameValues.push(valueOfTextbox + "#");
                $(this).css({
                    "border": "2px solid black", "background": "#EDFEED"
                });

                var address = $(this).val();
                var loc = [];

                geocoder.geocode({ 'address': address }, function (results, status) {
                    // and this is function which processes response
                    if (status == google.maps.GeocoderStatus.OK) {
                        loc[0] = results[0].geometry.location.lat();
                        loc[1] = results[0].geometry.location.lng();

                        arrayOfLatLngValues.push(loc[0] + ":" + loc[1]);
                        $("#hdnfldVariable").val(arrayOfLatLngValues);
                    }
                    else
                    {
                        alert("Geocode was not successful for the following reason: " + status);
                    }       
                });
            }
        });

        //If not all filled with details display message to user
        if (isValid == false)
        {
            alert("Fill In The " + array.length + " Empty Textboxs In Red And Then Verify To Continue.");
            e.preventDefault();
        }
        else
        {
            $("#numFldVariable").val(count);
            $("#returnDetailToDisplay").val(arrayOfDestinationNameValues);
            $("#optimiseButton").show();
        }
    });
});


window.setUpHandler = function (id) {

    var input = document.getElementById(id);

    var autocomplete = new google.maps.places.Autocomplete(input);
    autocomplete.bindTo('bounds', map);

    var infowindow = new google.maps.InfoWindow();
    var marker = new google.maps.Marker({
        map: map,
        anchorPoint: new google.maps.Point(0, -29)
    });

    autocomplete.addListener('place_changed', function () {
        infowindow.close();
        marker.setVisible(false);

        var place = autocomplete.getPlace();
        if (!place.geometry) {
            window.alert("Choose Destination From The Autocomplete Dropdown");
            input.style.backgroundColor = '#FFCECE';
            input.style.border = "2px solid red";
            input.value = "";
            return;
        }

        // If the place has a geometry, then present it on a map.
        if (place.geometry.viewport) {
            map.fitBounds(place.geometry.viewport);
            input.style.backgroundColor = '#EDFEED';
            input.style.fontWeight = "900";
            input.style.border = "2px solid black";
        } else {
            map.setCenter(place.geometry.location);
            map.setZoom(17);  // Why 17? Because it looks good.
            input.style.fontWeight = "900";
            input.style.backgroundColor = '#EDFEED';
            input.style.border = "2px solid black";
        }

        var latitude = place.geometry.location.lat();
        var longitude = place.geometry.location.lng();

        marker.setPosition(place.geometry.location);
        marker.setVisible(true);

        //Details on small info window over marker
        var address = '';
        if (place.address_components) {
            address = [
            (place.address_components[0] && place.address_components[0].short_name || ''),
            (place.address_components[1] && place.address_components[1].short_name || ''),
            (place.address_components[2] && place.address_components[2].short_name || '')
            ].join(' ');
        };

        infowindow.setContent('<div><strong>' + place.name + '</strong><br>' + address);
        infowindow.open(map, marker);


        infowindow.open(map, marker);
    });
}

//init's the first 5 autocomplete boxes
$(window).bind("load", function () {
    setUpHandler('tb_1');
    setUpHandler('tb_2');
    setUpHandler('tb_3');
    setUpHandler('tb_4');
    setUpHandler('tb_5');
});