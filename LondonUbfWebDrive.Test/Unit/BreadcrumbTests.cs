using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LondonUbfWebDrive.Domain;
using LondonUbfWebDrive.Repositories;
using Machine.Specifications;

namespace LondonUbfWebDrive.Test.Unit
{
    public class BreadcrumbTests
    {
        public class When_you_navigate_the_base_folder
        {
            static BreadcrumbMaker _breadcrumbMaker;
            static IEnumerable<Breadcrumb> _breakcrumbs;

            Establish context = () => _breadcrumbMaker = new BreadcrumbMaker();

            Because of = () => _breakcrumbs = _breadcrumbMaker.Make(string.Empty);

            It should_have_home = () => _breakcrumbs.First().Name.ShouldEqual("Home");
            It should_have_the_root_path = () => _breakcrumbs.First().Path.ShouldEqual("\\");
        }

        public class When_you_navigate_the_a_folder
        {
            private static BreadcrumbMaker _breadcrumbMaker;
            private static IEnumerable<Breadcrumb> _breakcrumbs;
            private static string _folderName = "Arrow.Season.1";

            Establish context = () => _breadcrumbMaker = new BreadcrumbMaker();

            Because of = () => _breakcrumbs = _breadcrumbMaker.Make(_folderName);

            It should_have_home = () => _breakcrumbs.First().Name.ShouldEqual("Home");
            It should_have_the_root_path = () => _breakcrumbs.First().Path.ShouldEqual("\\");
            It should_have_the_folder_name = () => _breakcrumbs.Skip(1).First().Name.ShouldEqual(_folderName);
            It should_have_the_folder_path = () => _breakcrumbs.Skip(1).First().Path.ShouldEqual("\\" + _folderName);
        }

        public class When_you_navigate_the_a_sub_folder
        {
            private static BreadcrumbMaker _breadcrumbMaker;
            private static IEnumerable<Breadcrumb> _breakcrumbs;
            private static string _folder = "Arrow.Season.1";
            private static string _subfolder = "subtitles";

            private static string _path = _folder + "/" + _subfolder;

            Establish context = () => _breadcrumbMaker = new BreadcrumbMaker();

            Because of = () => _breakcrumbs = _breadcrumbMaker.Make(_path);

            It should_have_home = () => _breakcrumbs.First().Name.ShouldEqual("Home");
            It should_have_the_root_path = () => _breakcrumbs.First().Path.ShouldEqual("\\");
            It should_have_the_folder_name = () => _breakcrumbs.Skip(1).First().Name.ShouldEqual(_folder);
            It should_have_the_folder_path = () => _breakcrumbs.Skip(1).First().Path.ShouldEqual("\\" + _folder);
            It should_have_the_subfolder_name = () => _breakcrumbs.Skip(2).First().Name.ShouldEqual(_subfolder);
            It should_have_the_subfolder_path = () => _breakcrumbs.Skip(2).First().Path.ShouldEqual("\\" + _folder + "\\" + _subfolder);
        }

    }
}
