<%@ Page Language="C#" AutoEventWireup="true" Inherits="OpenRasta.Codecs.WebForms.ResourceView<WebDrive.App.Resources.Home>" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="London Ubf Web Drive">
    <meta name="author" content="Andrew Chaa">
    <link rel="icon" type="image/png" href="/Images/diskdrive.png" />

    <title><%= Resource.Title %></title>

    <link href="../Content/bootstrap-responsive.min.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <style> body { padding-top: 60px; /* 60px to make the container go all the way to the bottom of the topbar */ } </style>
    
    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="../Scripts/html5shiv.js"></script>
    <![endif]-->

    <script src="../Scripts/jquery-1.9.1.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/knockout-2.2.1.js"></script>
    
    <script>
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-2526213-9', 'londonubf.homeip.net');
        ga('send', 'pageview');
    </script>

</head>
<body style="overflow-y: scroll;">
    
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="navbar-inner">
        <div class="container">
            <button type="button" class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse">
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            </button>
            <a class="brand" href="/">Web drive <span class="label label-success">Beta</span></a>
            <div class="nav-collapse collapse">
            <ul class="nav">
                <li class="active"><a href="/">Home</a></li>
                <li><a href="#about">Messages <span class="label label-info">not ready</span></a></li>
                <li><a href="#contact">Pictures <span class="label label-info">not ready</span></a></li>
            </ul>
            </div><!--/.nav-collapse -->
        </div>
        </div>
    </div>

    <div class="container"> <!-- container begin -->

        <p>
            Browse and downlaod files<br />
            If you find any issue or have suggestions, please let me know by email: <a href="mailto:andrew.chaa@yahoo.co.uk">andrew.chaa@yahoo.co.uk</a>.<br />
            If you hav a Github account, you can create an issue on <a href="https://github.com/andrewchaa/LondonUbfWebDrive/issues" target="_blank">Gibhub</a>.
        </p>

        <div class="row">
            <div class="span9">
                <ul class="breadcrumb" id="breaddcrumb" data-bind="foreach: breadcrumbs">
                    <li>
                        <a href="#" class="btn btn-info" data-bind="text: Name, click: $parent.clickBreadcrumb"></a>
                        <span class="divider">/</span>
                    </li>
                </ul>
                <table class="table table-hover table-striped table-condensed">
                    <tbody data-bind="foreach: documents" id="fileList">
                        <tr>
                            <td class="span1"><a href="#" data-bind="click: $parent.clickItem, attr: { title: Name }"><img data-bind="    attr: { src: ImagePath }" alt="icon" /></a></td>
                            <td><a href="#" data-bind="text: Name, click: $parent.clickItem, attr: { title: Name }"></a></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="span3">
                <p><span class="label label-success">Recent downloads</span></p>
                <table class="table table-bordered table-condensed">
                    <tbody data-bind="foreach: recentDownloads">
                        <tr class="success">
                            <td><a href="#" data-bind="text: Name, click: $parent.clickItem"></a></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>

        <script src="../Scripts/webdrive.js"></script>

    </div> <!-- container end -->


    <script src="../Scripts/webdrive.js"></script>

</body>
</html>
