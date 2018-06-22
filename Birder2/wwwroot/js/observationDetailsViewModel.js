ObservationDetailsViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);

    self.RecordDates = ko.pureComputed(function () {
        return "Observation created: " + moment(self.CreationDate()).format('DD/MM/YYYY HH:mm') + "; last update: " + moment(self.LastUpdateDate()).format('DD/MM/YYYY HH:mm');
    });

    self.Title = ko.pureComputed(function () {
        return ("<h1>" + self.Bird.EnglishName() + "<small><i> " + self.Bird.Species() + "</i></small></h1>");
    });

    self.SubTitle = ko.pureComputed(function () {
        var verb = self.Quantity > 1 ? "were" : "was";
        return ("<h4><b>" + self.Quantity() + "</b> " + verb + " spotted by <a asp-controller='User' asp-action='Details'>You</a> on " + moment(self.ObservationDateTime()).format('dddd, Do MMMM YYYY, HH:mm') + "</h4>");
    });

};

window.onload = function initMap() {
    var observationLocation = {
        lat: observationDetailsViewModel.LocationLatitude(), lng: observationDetailsViewModel.LocationLongitude()
    };

    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 7,
        center: observationLocation
    });

    var contentString = '<div id="content">' +
        '<div id="siteNotice">' +
        '</div>' +
        '<h4 id="firstHeading" class="firstHeading">@Model.Bird.EnglishName</h4>' +
        '<div id="bodyContent">' +
        '<p><b>Uluru</b>, also referred to as <b>Ayers Rock</b>' +
        '</div>' +
        '</div>';

    var infowindow = new google.maps.InfoWindow({
        content: contentString
    });

    var marker = new google.maps.Marker({
        position: observationLocation,
        map: map,
    });
    marker.addListener('click', function () {
        infowindow.open(map, marker);
    });

}

function showError(error) {
    switch (error.code) {
        case error.PERMISSION_DENIED:
            x.innerHTML = "User denied the request for Geolocation."
            break;
        case error.POSITION_UNAVAILABLE:
            x.innerHTML = "Location information is unavailable."
            break;
        case error.TIMEOUT:
            x.innerHTML = "The request to get user location timed out."
            break;
        case error.UNKNOWN_ERROR:
            x.innerHTML = "An unknown error occurred."
            break;
    }
}