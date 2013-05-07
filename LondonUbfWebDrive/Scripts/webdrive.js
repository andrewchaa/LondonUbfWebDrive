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
//        window.location.href = 'api/documents' + path;
    };
    self.getBreadcrumbs = function(path) {
        $.get('api/breadcrumbs' + path, function(data) {
            self.breadcrumbs(data);
        });
    };

};

$(function () {

    var viewModel = new DriveViewModel();
    ko.applyBindings(viewModel);

    viewModel.list();
    viewModel.getBreadcrumbs('/');

    $('#fileList').delegate('a', 'click', function (e) {
        e.preventDefault();

        var path = $(this).attr('data-path');
        console.log(path);

        var isFolder = $(this).attr('data-isfolder');
        if (!!isFolder) {
            viewModel.list(path);
            viewModel.getBreadcrumbs(path);
        } else {
            viewModel.get(path);
        }
            
    });

    $('#breaddcrumb').delegate('a', 'click', function(e) {
        e.preventDefault();

        var path = $(this).attr('data-path');
        viewModel.list(path);
        viewModel.getBreadcrumbs(path);
    });

});

