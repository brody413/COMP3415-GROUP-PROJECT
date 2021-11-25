// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let map;

/* Source: Google Maps API Overview 
 * https://developers.google.com/maps/documentation/javascript/markers#maps_marker_simple-javascript
 * https://developers.google.com/maps/documentation/javascript/overview?csw=1#maps_map_simple-html
 */ 

function initMap() {
    const loc = { lat: 44.38963, lng: -79.68212 };
    map = new google.maps.Map(document.getElementById('map'), {
        center: loc,
        zoom: 18
    });

    new google.maps.Marker({
        position: loc,
        map,
        title: "Lakeshore Hotel",
    });
}