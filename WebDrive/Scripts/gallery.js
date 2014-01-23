var ImageViewModel = function () {
    var self = this;
    self.filesUploaded = ko.observableArray();
    self.breadcrumbs = ko.observableArray();
    self.recentDownloads = ko.observableArray();
    self.images = ko.observableArray();

    self.list = function (directory) {
        self.images([]);

        var fileDirectoryUri = '/api/fileDirectory';
        if (directory) {
            fileDirectoryUri += '/' + directory;
        }

        $.get(fileDirectoryUri, function (entities) {
            $.each(entities, function (i, val) {
                console.log(val);
                $.get('/api/thumbnails/' + val.FullNameBase64, function (thumbnail) {
                    self.images.push(thumbnail);
                });
            });
        });
    };

    self.click = function (thumbnail) {
        if (!!thumbnail.IsDirectory) {
            self.list(thumbnail.FullNameBase64);
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

