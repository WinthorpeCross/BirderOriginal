CreateObservationViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, observedSpeciesMapping, self);

    self.addObservedSpecies = function () {
        var observedSpecies = new ObservedSpeciesViewModel({ Id: 0, BirdId: 0, Quantity: 1 });
        self.ObservedSpecies.push(observedSpecies);
    }

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
                var obj = JSON.parse(data);
                self.PONumber(obj.PONumber);
                self.CustomerName(obj.CustomerName);
                self.MessageToClient(obj.MessageToClient);
                self.SalesOrderId(obj.SalesOrderId);

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