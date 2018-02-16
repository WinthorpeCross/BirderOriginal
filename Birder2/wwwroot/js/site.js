// Write your JavaScript code.

SalesOrderItemViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);
}
