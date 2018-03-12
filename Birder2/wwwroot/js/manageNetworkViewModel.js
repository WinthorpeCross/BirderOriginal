ManageNetworkViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);


    self.FollowingCount = ko.observable(self.FollowingList().length);
    self.FollowersCount = ko.observable(self.FollowersList().length);

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
                    var obj = JSON.parse(data);
                    self.StatusMessage("You have unfollowed " + obj.UserName);
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
                url: "/Networks/Follow/",
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