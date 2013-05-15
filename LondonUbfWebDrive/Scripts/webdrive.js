var DriveViewModel = function () {
    var self = this;
    self.breadcrumbs = ko.observableArray();
    self.documents = ko.observableArray();
    self.list = function (path) {
        var uri = 'api/folders';
        if (!!path) {
            uri += path;
        }

        $.get(uri, function (data) {
            self.documents(data);
        });
    };
    
    self.get = function (path) {
        var hiddneIFrameId = 'hiddenDownloader',
            iframe = document.getElementById(hiddneIFrameId);
        
        if (iframe === null) {
            iframe = document.createElement('iframe');
            iframe.id = hiddneIFrameId;
            iframe.style.display = 'none';
            document.body.appendChild(iframe);
        }
        iframe.src = 'api/documents' + path;
        
    };

    self.getBreadcrumbs = function (path) {
        $.get('api/breadcrumbs' + path, function(data) {
            self.breadcrumbs(data);
        });
    };

    self.clickItem = function (item) {
        if (!!item.IsFolder) {
            self.list(item.FullName);
            self.getBreadcrumbs(item.FullName);
        } else {
            self.get(item.FullName);
            self.saveMetaData(item);
        }
    };

    self.clickBreadcrumb = function(item) {
        self.list(item.Path);
        self.getBreadcrumbs(item.Path);
    };

    self.saveMetaData = function(item) {
        //        $.post('webdriveapi.apphb.com', ko.toJSON(item));
        console.log(ko.toJSON(item));
    };

};

$(function () {

    var viewModel = new DriveViewModel();
    ko.applyBindings(viewModel);

    viewModel.list();
    viewModel.getBreadcrumbs('/');

});

