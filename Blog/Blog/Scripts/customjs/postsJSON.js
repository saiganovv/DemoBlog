
var PostViewModel = function () {
    var self = this;
    self.Id = ko.observable("0");
    self.Title = ko.observable("");
    self.Description = ko.observable("");
    self.Data = ko.observable("");


    var PostData = {
        Id: self.Id,
        Title: self.Title,
        Description: self.Description,
        Data: self.Data
    };


    self.AllPosts = ko.observableArray([]);

    GetPosts();

    self.save = function () {
        $.ajax({
            type: "POST",
            url: "/api/PostsAPI",
            data: ko.toJSON(PostData),
            contentType: "application/json",
            success: function (data) {
                toastr.success("Success");
                self.Id(data.Id);
                GetPosts();
            },
            error: function () {
                toastr.warning("Failed");
            }
        });
    };

    self.update = function () {
        var url = "/api/PostsAPI/" + self.Id();
        alert(url);
        $.ajax({
            type: "PUT",
            url: url,
            data: ko.toJSON(PostData),
            contentType: "application/json",
            success: function (data) {
                toastr.success("Success");
                GetPosts();
            },
            error: function (error) {
                toastr.success("Failed");
            }
        });
    };

    self.deleterec = function (post) {
        $.ajax({
            type: "DELETE",
            url: "/api/PostsAPI/" + post.ID,
            success: function (data) {
                toastr.success("Success");
                GetPosts();
            },
            error: function (error) {
                toastr.success("Failed");
            }
        });
    };

  
    function GetPosts() {
        $.ajax({
            type: "GET",
            url: "/api/PostsAPI",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                self.AllPosts(data);
            },
            error: function (error) {
                toastr.success("Failed");
            }
        });
    }

    self.getselectedpost = function (post) {
        self.Id(post.Id),
        self.Title(post.Title),
        self.Description(post.Description);
        self.Data(post.Data);
    };


};
ko.applyBindings(new PostViewModel());