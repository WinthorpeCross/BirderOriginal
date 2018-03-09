ManageNetworkViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);


    self.FollowingCount = ko.observable(self.Following().length);
    self.FollowersCount = ko.observable(self.Followers().length);

    self.unFollow = function (data, event) {
        if (event.target.innerText === 'Unfollow') {
            //alert("Unfollow route");

            $.ajax({
                url: "/Networks/UnFollow/",
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
                        $('#MessageToClient').text(XMLHttpRequest.responseText);
                    }
                    else {
                        $('#MessageToClient').text('The web server had an error.  The issue has been logged for investigation by the developer.');
                    }
                }
            });
        }
        else {
            //alert("Follow route?");

            $.ajax({
                url: "/Networks/Follow2/",
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
    };
};