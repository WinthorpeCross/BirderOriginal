CreateObservationViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, observedSpeciesMapping, self);

    ko.bindingHandlers.selectPicker = {
        init: function (element, valueAccessor, allBindings) {
            $(element).selectpicker('render');
        }
    };

    ko.bindingHandlers.datepicker = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            //initialize datepicker with some optional options
            var options = {
                format: 'DD/MM/YYYY HH:mm',
                defaultDate: valueAccessor()()
            };

            if (allBindingsAccessor() !== undefined) {
                if (allBindingsAccessor().datepickerOptions !== undefined) {
                    options.format = allBindingsAccessor().datepickerOptions.format !== undefined ? allBindingsAccessor().datepickerOptions.format : options.format;
                }
            }

            $(element).datetimepicker(options);

            //when a user changes the date, update the view model
            ko.utils.registerEventHandler(element, "dp.change", function (event) {
                var value = valueAccessor();
                if (ko.isObservable(value)) {
                    value(event.date);
                }
            });

            var defaultVal = $(element).val();
            var value = valueAccessor();
            value(moment(defaultVal, options.format));
        },
        update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var thisFormat = 'DD/MM/YYYY HH:mm';

            if (allBindingsAccessor() !== undefined) {
                if (allBindingsAccessor().datepickerOptions !== undefined) {
                    thisFormat = allBindingsAccessor().datepickerOptions.format !== undefined ? allBindingsAccessor().datepickerOptions.format : thisFormat;
                }
            }

            var value = valueAccessor();
            var unwrapped = ko.utils.unwrapObservable(value());

            if (unwrapped === undefined || unwrapped === null) {
                element.value = new moment(new Date());
                console.log("undefined");
            } else {
                element.value = unwrapped.format(thisFormat);
            }
        }
    };

    self.addObservedSpecies = function () {
        var observedSpecies = new ObservedSpeciesViewModel({ BirdId: 0, Quantity: 1 });
        self.ObservedSpecies.push(observedSpecies);
    };

    self.Total = ko.computed(function () {
        var total = 0;
        total += self.ObservedSpecies().length;
        return total;
    }),

    self.post = function () {
        $.ajax({
            url: "/Observation/Post/",
            type: "POST",
            data: ko.toJSON(self),
            headers:
            {
                "content-type": "application/json; charset=utf-8"
            },
            success: function (data) {
                //alert("success");
                //var obj = JSON.parse(data);

                window.location.replace("./Index/"); // + obj.SalesOrderId);

                //alert("success");
                //console.log(obj);
                //ko.fromJS(data.salesOrderViewModel, {}, self);
                //if (data.SalesOrderViewModel)
            },
            error: function (textStatus, errorThrown) {
                alert("error");
                //window.location.replace("./Details/" + obj.SalesOrderId);
                //redirect to an error page?
            }//,
            //always: function (data) {
            //},
        });
    };
};

var observedSpeciesMapping = {
    'ObservedSpecies': {
        key: function (obsevedSpecies) {
            return ko.utils.unwrapObservable(obsevedSpecies.Id);
        },
        create: function (options) {
            return new CreateObservationViewModel(options.data);
        }
    }
};

ObservedSpeciesViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, observedSpeciesMapping, self);  
};


$("#form").validate({
    submitHandler: function () {
        createObservationViewModel.post();
    },

    rules: {
        "Observation.Note": {
            required: true
        //Obervation.Note: {
        //    required: true
        },
        "Observation.BirdId": {
            required: true
            //Obervation.Note: {
            //    required: true
        },
        "Observation.ObservationDateTime": {
            //required: true,
            date: true

            //Obervation.Note: {
            //    required: true
        },

    }
});



