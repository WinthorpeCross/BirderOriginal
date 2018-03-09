FollowUserViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, searchResultsMapping, self);

    self.follow = function (data, event) {
        if (event.target.innerText === 'Follow') {
            //alert("Follow route");

            $.ajax({
                url: "/Networks/Follow/",
                type: "POST",
                data: ko.toJSON(data),
                headers:
                    {
                        "content-type": "application/json; charset=utf-8"
                    },
                success: function (data) {
                    //alert("success");
                    event.target.innerText = 'Unfollow';
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("error");
                    if (XMLHttpRequest.status === 400) {
                        alert(XMLHttpRequest.responseText);
                        $('#MessageToClient').text(XMLHttpRequest.responseText);
                    }
                    else {
                        $('#MessageToClient').text('The web server had an error.  The issue has been logged for investigation by the developer.');
                    }
                }
            })
        }
        else {
            //alert("Unfollow route?");

            $.ajax({
                url: "/Networks/UnFollow2/",
                type: "POST",
                data: ko.toJSON(data),
                headers:
                    {
                        "content-type": "application/json; charset=utf-8"
                    },
                success: function (data) {
                    //alert("success");
                    event.target.innerText = 'Follow';
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("error");
                    if (XMLHttpRequest.status === 400) {
                        alert(XMLHttpRequest.responseText);
                        $('#MessageToClient').text(XMLHttpRequest.responseText);
                    }
                    else {
                        $('#MessageToClient').text('The web server had an error.  The issue has been logged for investigation by the developer.');
                    }
                }
            })
        }
    }
};

var searchResultsMapping = {
    'SearchResults': {
        key: function (searchResult) {
            return ko.utils.unwrapObservable(searchResult.Id);
        }
        //create: function (options) {
        //    return new CreateObservationViewModel(options.data);
        //}
    }
};

SearchResultsViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, searchResultsMapping, self);
};