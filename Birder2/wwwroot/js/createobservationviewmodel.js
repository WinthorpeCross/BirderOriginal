CreateObservationViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, observedSpeciesMapping, self);

    self.addObservedSpecies = function () {
        var observedSpecies = new ObservedSpeciesViewModel({ Id: 0, /*Bird: new Bird,*/ Quantity: 1 });// ObjectState: ObjectState.Added });
        self.ObservedSpecies.push(observedSpecies);
    }
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