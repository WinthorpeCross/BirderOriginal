CreateObservationViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, observedSpeciesMapping, self);

    ko.bindingHandlers.selectPicker = {
        init: function (element, valueAccessor, allBindings) {
            $(element).selectpicker('render');
        }
    };

    self.addObservedSpecies = function () {
        var observedSpecies = new ObservedSpeciesViewModel({ Id: 0, BirdId: 0, Quantity: 1 });
        self.ObservedSpecies.push(observedSpecies);
    };

    self.removeObservedSpecies = function () {
        self.ObservedSpecies.pop();
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
        //"Observation.NoteGeneral": {
        //    required: true
        //},
        Quantity: {
            required: true,
            digits: true,
            range: [0, 100000]
        },
        "Observation.BirdId": {
            required: true
        },
        "Observation.ObservationDateTime": {
            date: true
        },
    },

    messages: {
        "Observation.BirdId": {
            required: "You must choose a bird species."//,
            //maxlength: "Customer names must be 30 characters or shorter."
        },
        //Quantity: {
        //    required: "Quantity must be digits only",
        //    digitsonly: "k"
        //}
    },

    tooltip_options: {
        Quantity: {
            placement: 'right'
        },
        PONumber: {
            placement: 'right'
        }
    }
});


// example of custom validation
$.validator.addMethod("alphaonly",
    function (value) {
        return /^[A-Za-z]+$/.test(value);
    }
);




