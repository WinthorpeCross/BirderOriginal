FollowUserViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, searchResultsMapping, self);

    self.follow = function (data, event) {
        if (event.target.innerText === 'Follow') {
            //alert("Follow route");

            $.ajax({
                url: "/Network/Follow/",
                type: "POST",
                data: ko.toJSON(data),
                headers:
                    {
                        "content-type": "application/json; charset=utf-8"
                    },
                success: function (data) {
                    //alert("success");
                    var obj = JSON.parse(data);
                    self.StatusMessage("You are now following " + obj.UserName);
                    event.target.innerText = 'Unfollow';
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    self.StatusMessage("An error occurred");
                    if (XMLHttpRequest.status === 400) {
                        alert(XMLHttpRequest.responseText);
                        $('#StatusMessage').text(XMLHttpRequest.responseText);
                    }
                    else {
                        $('#StatusMessage').text('The web server had an error.  The issue has been logged for investigation by the developer.');
                    }
                }
            })
        }
        else {
            $.ajax({
                url: "/Network/UnFollow/",
                type: "POST",
                data: ko.toJSON(data),
                headers:
                    {
                        "content-type": "application/json; charset=utf-8"
                    },
                success: function (data) {
                    //alert("success");
                    event.target.innerText = 'Follow';
                    var obj = JSON.parse(data);
                    self.StatusMessage("You have unfollowed " + obj.UserName);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    self.StatusMessage("An error occurred");
                    if (XMLHttpRequest.status === 400) {
                        alert(XMLHttpRequest.responseText);
                        $('#StatusMessage').text(XMLHttpRequest.responseText);
                    }
                    else {
                        $('#StatusMessage').text('The web server had an error.  The issue has been logged for investigation by the developer.');
                    }
                }
            })
        }
    }
};

var searchResultsMapping = {
    'SearchResults': {
        key: function (searchResult) {
            return ko.utils.unwrapObservable(searchResult.UserName);
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