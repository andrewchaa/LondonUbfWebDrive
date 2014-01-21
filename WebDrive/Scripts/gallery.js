var ImageViewModel = function () {
    var self = this;
    self.filesUploaded = ko.observableArray();
    self.breadcrumbs = ko.observableArray();
    self.recentDownloads = ko.observableArray();
    self.images = ko.observableArray();

    self.list = function (directory) {
        var fileDirectoryUri = '/api/fileDirectory';
        if (directory) {
            fileDirectoryUri += '/' + directory;
        }

        $.get(fileDirectoryUri, function (data) {
            $.each(data, function (i, val) {
                $.get('/api/thumbnails/' + val.FullNameBase64, function(thumbnails) {
                    console.log('test');
                });
//                self.images.push(val);
            });

            //                self.images(data);
        });
    };

    self.click = function (thumbnail) {
        if (!!thumbnail.IsDirectory) {
            self.list(thumbnail.RelativePath);
            self.getBreadcrumbs(thumbnail.RelativePath);
        }
        console.log(thumbnail);
    };

    self.getBreadcrumbs = function (path) {
        $.get('/api/breadcrumbs/' + path, function (data) {
            self.breadcrumbs(data);
        });
    };

    self.clickBreadcrumb = function (item) {
        self.list(item.Path);
        self.getBreadcrumbs(item.Path);
    };

};

$(function () {
    var viewmodel = new ImageViewModel();
    ko.applyBindings(viewmodel);

    viewmodel.list();
    viewmodel.getBreadcrumbs('/');


});

