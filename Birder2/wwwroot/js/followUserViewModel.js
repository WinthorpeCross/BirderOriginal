FollowUserViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);

    self.buttonText = ko.observable('Add');
    //self.FollowingCount = ko.observable(self.Following().length);
    //self.FollowersCount = ko.observable(self.Followers().length);

    self.follow = function (data) {
        console.log(data);
        alert("hello there!");

        $.ajax({
            url: "/Networks/Follow/",
            type: "POST",
            data: ko.toJSON(data),
            headers:
            {
                "content-type": "application/json; charset=utf-8"
            },
            success: function (data) {
                alert("success");
                //var obj = JSON.parse(data);
                //if (obj.IsModelStateValid === false) {
                //    self.MessageToClient(obj.MessageToClient);
                //}
                //else {
                //    window.location.replace("./Index/");
                //}
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
        });
    };
};