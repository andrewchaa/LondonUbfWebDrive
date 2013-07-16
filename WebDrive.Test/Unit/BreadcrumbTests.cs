using System.Collections.Generic;
using System.Linq;
using LondonUbfWebDrive.Domain.Model;
using LondonUbfWebDrive.Domain.Services;
using Machine.Specifications;

namespace LondonUbfWebDrive.Test.Unit
{
    public class BreadcrumbTests
    {
        public class When_you_navigate_the_base_folder
        {
            static BreadcrumbService _breadcrumbService;
            static IEnumerable<Breadcrumb> _breakcrumbs;

            Establish context = () => _breadcrumbService = new BreadcrumbService();

            Because of = () => _breakcrumbs = _breadcrumbService.ConvertFrom(string.Empty);

            It should_have_home = () => _breakcrumbs.First().Name.ShouldEqual("Home");
        }

        public class When_you_navigate_the_a_folder
        {
            private static BreadcrumbService _breadcrumbService;
            private static IEnumerable<Breadcrumb> _breakcrumbs;
            private static string _folderName = "Arrow.Season.1";

            Establish context = () => _breadcrumbService = new BreadcrumbService();

            Because of = () => _breakcrumbs = _breadcrumbService.ConvertFrom(_folderName);

            It should_have_home = () => _breakcrumbs.First().Name.ShouldEqual("Home");
            It should_have_the_folder_name = () => _breakcrumbs.Skip(1).First().Name.ShouldEqual(_folderName);
            It should_have_the_folder_path = () => _breakcrumbs.Skip(1).First().Path.ShouldEqual("\\" + _folderName);
        }

        public class When_you_navigate_the_a_sub_folder
        {
            private static BreadcrumbService _breadcrumbService;
            private static IEnumerable<Breadcrumb> _breakcrumbs;
            private static string _folder = "Arrow.Season.1";
            private static string _subfolder = "subtitles";

            private static string _path = _folder + "/" + _subfolder;

            Establish context = () => _breadcrumbService = new BreadcrumbService();

            Because of = () => _breakcrumbs = _breadcrumbService.ConvertFrom(_path);

            It should_have_home = () => _breakcrumbs.First().Name.ShouldEqual("Home");
            It should_have_the_folder_name = () => _breakcrumbs.Skip(1).First().Name.ShouldEqual(_folder);
            It should_have_the_folder_path = () => _breakcrumbs.Skip(1).First().Path.ShouldEqual("\\" + _folder);
            It should_have_the_subfolder_name = () => _breakcrumbs.Skip(2).First().Name.ShouldEqual(_subfolder);
            It should_have_the_subfolder_path = () => _breakcrumbs.Skip(2).First().Path.ShouldEqual("\\" + _folder + "\\" + _subfolder);
        }

    }
}
