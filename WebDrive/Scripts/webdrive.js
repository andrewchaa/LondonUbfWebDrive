var DriveViewModel = function () {
    var self = this;
    self.breadcrumbs = ko.observableArray();
    self.documents = ko.observableArray();
    self.recentDownloads = ko.observableArray();
    self.filesUploaded = ko.observableArray();
    self.path = '';

    self.getPath = function() {
        return self.path || '';
    };
    
    self.list = function (path) {
        var uri = 'api/folders';
        if (!!path) {
            uri += path;
        }

        $.get(uri, function (data) {
            self.documents(data);
        });

        self.path = path;
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

    self.getPopularDownload = function() {
        $.get('api/recentdownloads', function(data) {
            self.recentDownloads(data);
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
        $.post('api/metadata', "=" + ko.toJSON(item));
    };

    self.displayUploadDoneFor = function(file) {
        console.log(file);
        self.filesUploaded.push(file);
        self.list(self.path);
    };

    self.clearUploadDone = function() {
        self.filesUploaded.removeAll();
    };

};

$(function () {

    var viewModel = new DriveViewModel();
    ko.applyBindings(viewModel);

    viewModel.list();
    viewModel.getBreadcrumbs('/');
    viewModel.getPopularDownload();
    
    $('#btnUpload').click(function () {
        $('#dropbox').modal();
    });

    var dropbox = $('#dropbox');

    dropbox.filedrop({
        paramname: 'uploadFiles',
        maxfiles: 5,
        maxfilesize: 2, // in mb
        url: '/api/documents',
        data: {
            selectedDir: function () {
                return viewModel.getPath();
            }
        },
        uploadFinished: function (i, file, response) {
            $('#dropbox').modal('hide');
            $('#msgUploadDone').fadeIn();

            viewModel.displayUploadDoneFor(file);
            setTimeout(function () {
                $('#msgUploadDone').fadeOut();
                viewModel.clearUploadDone();
            }, 3000);

        },

        error: function (err, file) {
            switch (err) {
                case 'BrowserNotSupported':
                    showMessage('Your browser does not support HTML5 file uploads!');
                    break;
                case 'TooManyFiles':
                    alert('Too many files! Please select 5 at most!');
                    break;
                case 'FileTooLarge':
                    alert(file.name + ' is too large! Please upload files up to 2mb.');
                    break;
                default:
                    break;
            }
        },

        // Called before each upload is started
        beforeEach: function (file) {
            path = $('#btnUpload').attr('data-path');
            console.log(path);
        },

        uploadStarted: function (i, file, len) {
            //                createImage(file);
        },

        progressUpdated: function (i, file, progress) {
            //                $.data(file).find('.progress').width(progress);
        }

    });

    var template = '...';


    function showMessage(msg) {
        //            message.html(msg);
    }


});

