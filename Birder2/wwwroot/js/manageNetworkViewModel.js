ManageNetworkViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);

    self.FollowingCount = ko.observable(self.FollowingList().length);
    self.FollowersCount = ko.observable(self.FollowersList().length);

    self.unFollow = function (data, event) {
        if (event.target.innerText === 'Unfollow') {
            $.ajax({
                url: "/Network/UnFollow/",
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
                    //alert("error");
                    self.StatusMessage("An error occurred");
                    if (XMLHttpRequest.status === 400) {
                        $('#StatusMessage').text(XMLHttpRequest.responseText);
                    }
                    else {
                        $('#StatusMessage').text('The web server had an error.  The issue has been logged for investigation by the developer.');
                    }
                }
            });
        }
        else {
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
    };
};