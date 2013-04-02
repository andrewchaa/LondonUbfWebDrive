using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LondonUbfWebDrive.Domain;
using LondonUbfWebDrive.Repositories;
using Machine.Specifications;

namespace LondonUbfWebDrive.Test.Integrations
{
    public class BreadCrumbRepositoryTests
    {
        protected static IBreadcrumbRepository Repository;
        protected static string BaseFolder;
        protected static IEnumerable<Breadcrumb> Breadcrumbs;
        protected static string SubFolderPath;
        protected static string SubSubFolderPath;
        protected static string SubFolderName;
        protected static string SubSubFolderName;

        private Establish context = () =>
            {
                BaseFolder =  Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                SubFolderName = "Temp1";
                SubFolderPath = "\\" + SubFolderName;
                SubSubFolderName = "Temp2";
                SubSubFolderPath = SubFolderName + "\\" + SubSubFolderName;
            };
    }

    [Subject(typeof(BreadcrumbRepository))]
    public class When_I_browse_base_folder : BreadCrumbRepositoryTests
    {
        Establish context = () => Repository = new BreadcrumbRepository(BaseFolder);

        Because Base_folder_is_a_home_directory = () => Breadcrumbs = Repository.List("\\");

        It should_have_only_home_in_the_breadcrumb = () => Breadcrumbs.Count().ShouldEqual(1);
        It should_have_Home_Breadcrumb_Name = () => Breadcrumbs.First().Name.ShouldEqual("Home");
        It should_have_Home_Breadcrumb_Path = () => Breadcrumbs.First().Path.ShouldEqual("\\");
    }

    [Subject(typeof(BreadcrumbRepository))]
    public class When_I_browse_a_sub_folder : BreadCrumbRepositoryTests
    {
        Establish context = () =>
            {
                Directory.CreateDirectory(SubFolderPath);
                Repository = new BreadcrumbRepository(BaseFolder);
            };

        Because it_is_a_sub_folder = () => Breadcrumbs = Repository.List(Path.Combine(BaseFolder, SubFolderPath));

        It should_have_home_and_subfolder_in_the_breadcrumb = () => Breadcrumbs.Count().ShouldEqual(2);
        It should_have_Home_Breadcrumb = () => Breadcrumbs.First().Name.ShouldEqual("Home");
        It should_have_Sub_Folder_Breadcrumb_Name = () => Breadcrumbs.Skip(1).First().Name.ShouldEqual(SubFolderName);
        It should_have_Sub_Folder_Breadcrumb_Path = () => Breadcrumbs.Skip(1).First().Path.ShouldEqual(SubFolderPath);

        Cleanup delete_sub_folder = () => Directory.Delete(Path.Combine(BaseFolder, SubFolderPath));
    }

    [Subject(typeof(BreadcrumbRepository))]
    public class When_I_browse_a_sub_sub_folder : BreadCrumbRepositoryTests
    {
        Establish context = () =>
            {
                Directory.CreateDirectory(SubFolderPath);
                Repository = new BreadcrumbRepository(BaseFolder);
            };

        Because it_is_a_sub_folder = () => Breadcrumbs = Repository.List(Path.Combine(BaseFolder, SubSubFolderPath));

        It should_have_home_subfolder_and_subsubFolder_in_the_breadcrumb = () => Breadcrumbs.Count().ShouldEqual(3);
        It should_have_SubSubFolder_Breadcrumb_Name = () => Breadcrumbs.Skip(2).First().Name.ShouldEqual(SubSubFolderName);

        Cleanup delete_sub_folder = () => Directory.Delete(Path.Combine(BaseFolder, SubFolderPath));
    }


}