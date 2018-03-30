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

    //moment(baseModel.actionDate()).format('LL')


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



        //self.addObservedSpecies = function () {
        //    var observedSpecies = new ObservedSpeciesViewModel({ Id: 0, BirdId: 0, Quantity: 1 });
        //    self.ObservedSpecies.push(observedSpecies);
        //};

    self.disableSubmitButton = ko.observable(false);

        self.post = function () {
            self.disableSubmitButton(true);
            if (self.ObservedSpecies().length < 1) {
                // ToDo: Implement proper client-side validation of the Observed Species collection
                alert("You must choose at least one observed bird species");
                self.MessageToClient("You must choose at least one observed bird species...");
                self.disableSubmitButton(false);
                return;
            }
            $.ajax({
                url: "/Observation/Post/",
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
                        window.location.replace("./Index/");
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


//$("#form").validate({
//    submitHandler: function () {
//        editObservationViewModel.post();
//    },

//    rules: {
//        //ObservedSpecies: {
//        //    min: 1
//        //},
//        Quantity: {
//            required: true,
//            digits: true,
//            min: 1
//        },
//        "Observation.BirdId": {
//            required: true
//        },
//        "Observation.ObservationDateTime": {
//            date: true
//        }
//    },

//    messages: {
//        "Observation.BirdId": {
//            required: "You must choose a bird species."//,
//            //maxlength: "Customer names must be 30 characters or shorter."
//        },
//        Quantity: {
//            required: "Quantity must be digits only",
//            digitsonly: "k"
//        }
//    },

//    tooltip_options: {
//        Quantity: {
//            placement: 'bottom'
//        }
//        //PONumber: {
//        //    placement: 'right'
//        //}
//    }
//});


//// example of custom validation
//$.validator.addMethod("alphaonly",
//    function (value) {
//        return /^[A-Za-z]+$/.test(value);
//    }
//);