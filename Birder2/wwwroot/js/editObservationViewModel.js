EditObservationViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);

    self.RecordDates = ko.pureComputed(function () {
        return "Observation created: " + moment(self.Observation.CreationDate()).format('DD/MM/YYYY HH:mm') + "; last update: " + moment(self.Observation.LastUpdateDate()).format('DD/MM/YYYY HH:mm');
    });

    self.Title = ko.pureComputed(function () {
        return ("<h1>" + self.Observation.Bird.EnglishName() + "<small><i> " + self.Observation.Bird.Species() + "</i></small></h1>");
    });

    self.SubTitle = ko.pureComputed(function () {
        return ("<h4>Spotted on " + moment(self.Observation.ObservationDateTime()).format('dddd, Do MMMM YYYY, HH:mm') + "</h4>");
    });

    self.disableSubmitButton = ko.observable(false);

    ko.bindingHandlers.selectPicker = {
        init: function (element, valueAccessor, allBindings) {
            $(element).selectpicker('render');
        }
    };

    ko.bindingHandlers.dateTimePicker = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            //initialize datepicker with some optional options
            var options = allBindingsAccessor().dateTimePickerOptions || {};
            options['format'] = options['format'] || "dddd, Do MMMM YYYY, HH:mm"; //default format
            options.ignoreReadonly = true;
            options.defaultDate = valueAccessor()();
            $(element).datetimepicker(options);

            //when a user changes the date, update the view model
            ko.utils.registerEventHandler(element, "dp.change", function (event) {
                ignoreReadonly: true;
                var value = valueAccessor();
                if (ko.isObservable(value)) {
                    if (event.date !== null && !(event.date instanceof Date)) {
                        value(event.date.toDate());
                    } else {
                        value(event.date);
                    }
                }
            });

            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                var picker = $(element).data("DateTimePicker");
                if (picker) {
                    picker.destroy();
                }
            });
        },
        update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {

            var picker = $(element).data("DateTimePicker");
            //when the view model is updated, update the widget
            if (picker) {
                var koDate = ko.utils.unwrapObservable(valueAccessor());

                //in case return from server datetime i am get in this form for example /Date(93989393)/ then fomat this
                koDate = (typeof (koDate) !== 'object') ? new Date(parseFloat(koDate.replace(/[^0-9]/g, ''))) : koDate;

                picker.date(koDate);
            }
        }
    };

    self.post = function () {
        self.disableSubmitButton(true);
        console.log(self);
        $.ajax({
            url: "/Observation/Edit/",
            type: "POST",
            data: ko.toJSON(self),
            headers:
                {
                    "content-type": "application/json; charset=utf-8"
                },
            success: function (data) {
                var obj = JSON.parse(data);
                if (obj.IsModelStateValid === false) {
                    self.MessageToClient(obj.MessageToClient);
                }
                else {
                    window.location.href = '/Observation/Details/' + obj.Observation.ObservationId;
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                self.disableSubmitButton(false);
                if (XMLHttpRequest.status === 400) {
                    $('#MessageToClient').text(XMLHttpRequest.responseText);
                }
                else {
                    $('#MessageToClient').text('The web server had an error.  The issue has been logged for investigation by the developer.');
                }
            }
        });
    };
};


$("#form").validate({
    submitHandler: function () {
        editObservationViewModel.post();
    },

    rules: {
        //ObservedSpecies: {
        //    min: 1
        //},
        Quantity: {
            required: true,
            digits: true,
            min: 1
        },
        "Observation.BirdId": {
            required: true
        },
        "Observation.ObservationDateTime": {
            date: true
        }
    },

    messages: {
        "Observation.BirdId": {
            required: "You must choose a bird species."//,
            //maxlength: "Customer names must be 30 characters or shorter."
        },
        Quantity: {
            required: "Quantity must be digits only",
            digitsonly: "k"
        }
    },

    tooltip_options: {
        Quantity: {
            placement: 'bottom'
        }
        //PONumber: {
        //    placement: 'right'
        //}
    }
});


// example of custom validation
$.validator.addMethod("alphaonly",
    function (value) {
        return /^[A-Za-z]+$/.test(value);
    }
);

var markers = [];

function initMap() {
    var myLatlng = new google.maps.LatLng(editObservationViewModel.Observation.LocationLatitude(),editObservationViewModel.Observation.LocationLongitude());
    var myOptions = {
        zoom: 13,
        center: myLatlng,
    }

    var map = new google.maps.Map(document.getElementById("map"), myOptions);
    var geocoder = new google.maps.Geocoder();

    //google.maps.event.addListenerOnce(map, 'idle', addMarker(myLatlng));
    google.maps.event.addListenerOnce(map, 'idle', function (event) {
        geocoder.geocode({
            'latLng': myLatlng
        }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                if (results[0]) {
                    //alert(results[0].formatted_address);
                    document.getElementById('set').value = results[0].formatted_address
                    addMarker(results[0].geometry.location);
                }
            }
        });
    });

    document.getElementById('submit').addEventListener('click', function (event) {
        var address = document.getElementById('address').value;
        geocoder.geocode({
            'address': address
        }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                if (results[0]) {
                    //alert(results[0].formatted_address);
                    document.getElementById('set').value = results[0].formatted_address
                    addMarker(results[0].geometry.location);
                }
            }
        });
    });

    google.maps.event.addListener(map, 'click', function (event) {
        geocoder.geocode({
            'latLng': event.latLng
        }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                if (results[0]) {
                    //alert(results[0].formatted_address);
                    document.getElementById('set').value = results[0].formatted_address
                    addMarker(event.latLng);
                }
            }
        });
    });

    function addMarker(location) {
        var marker = new google.maps.Marker({
            position: location,
            map: map,
        });

        map.panTo(location);
        deleteMarkers();
        markers.push(marker);
        updateForm();
    }

    // Sets the map on all markers in the array.
    function setMapOnAll(map) {
        for (var i = 0; i < markers.length; i++) {
            markers[i].setMap(map);
        }
    }

    // Removes the markers from the map, but keeps them in the array.
    function clearMarkers() {
        setMapOnAll(null);
    }

    // Shows any markers currently in the array.
    function showMarkers() {
        setMapOnAll(map);
    }

    // Deletes all markers in the array by removing references to them.
    function deleteMarkers() {
        clearMarkers();
        markers = [];
    }
    function updateForm() {
        editObservationViewModel.Observation.LocationLatitude(markers[0].position.lat());
        editObservationViewModel.Observation.LocationLongitude(markers[0].position.lng());
        //document.getElementById('latitude').value = markers[0].position.lat();
        //document.getElementById('longitude').value = markers[0].position.lng();
    }
};