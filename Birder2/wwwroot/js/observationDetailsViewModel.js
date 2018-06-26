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
        var conjugatedVerb = self.Quantity > 1 ? "were" : "was";
        return ("<h4><b>" + self.Quantity() + "</b> " + verb + " spotted by <a asp-controller='User' asp-action='Details'>You</a> on " + moment(self.ObservationDateTime()).format('dddd, Do MMMM YYYY, HH:mm') + "</h4>");
    });

    function x() {

    }
};


window.onload = function initPage() {

    //Collapse photographs panel if no photos are available
    //alert(observationDetailsViewModel.HasPhotos());
    if (observationDetailsViewModel.HasPhotos() == false)
        $('#collapse1').collapse('hide');

    //
    fetchImageLinks(observationDetailsViewModel.ObservationId());

    // init Map
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

var gallery;

// Grab links for images from backend api
function fetchImageLinks(data) {
    $.get("/api/ImagesApi/thumbnails?observationId=" + data, function (fetchedImageLinks) {
        console.log(fetchedImageLinks)

        // Check if anything is in there
        if (_.isEmpty(fetchedImageLinks)) {
            console.log('empty fetched')
            // do nothing
        } else {
            // Check if we have a gallery initialized
            if (_.isEmpty(gallery)) {
                // initialize gallery
                gallery = blueimp.Gallery(
                    fetchedImageLinks, // gallery links array
                    {
                        container: '#blueimp-gallery-carousel',
                        carousel: true
                    } // gallery options
                );
            } else {
                // check if images are equal to array
                console.log('currently in gallery:')
                console.log(gallery.list)
                var imageLinksEqual = _.isEqual(_.sortBy(gallery.list.map(s => s.split("?")[0])), _.sortBy(fetchedImageLinks.map(s => s.split("?")[0])))
                if (imageLinksEqual) {
                    console.log('images arr are equal')
                    // do nothing
                } else {
                    console.log('images arr are not equal')

                    // update gallery with new image urls. Only compare actual url without SAS token query string
                    var newImageLinks = _.difference(fetchedImageLinks.map(s => s.split("?")[0]), gallery.list.map(s => s.split("?")[0]))

                    console.log('differene is: ')
                    console.log(newImageLinks)
                    // Only add new images
                    gallery.add(newImageLinks);

                    // Force image load
                    gallery.next();
                }
            }
        }
    });
}