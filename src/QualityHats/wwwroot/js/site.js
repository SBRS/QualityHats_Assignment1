// Write your Javascript code.

function initMap() {
    var coordinates = { lat: -36.880912, lng: 174.707811 };
	var map = new google.maps.Map(document.getElementById("googleMap"), {
		center: coordinates,
		zoom: 15
	});
	var contentString = '<div id="content">'+
		'<div id="siteNotice">'+
		'</div>'+
		'<h5 id="firstHeading" class="firstHeading">Quality Hats</h5>'+
		'<div id="bodyContent">'+
		'<p>139 Carrington Rd, Mount Albert, Auckland 1025</p>' +
		'</div>'+
		'</div>';
	var infowindow = new google.maps.InfoWindow({
		content: contentString
	});
	var marker = new google.maps.Marker({
	position: coordinates,
	map: map,
	animation: google.maps.Animation.DROP
	});
	marker.addListener('click', function() {
	infowindow.open(map, marker);
	});
}
