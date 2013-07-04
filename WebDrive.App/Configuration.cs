using System.Collections.Generic;
using LondonUbfWebDrive.Domain;
using LondonUbfWebDrive.Domain.Model;
using OpenRasta.Configuration;
using WebDrive.App.Handlers;
using WebDrive.App.Resources;

namespace WebDrive.App
{
    public class Configuration : IConfigurationSource
    {
        public void Configure()
        {
            using (OpenRastaConfiguration.Manual)
            {
                ResourceSpace.Has.ResourcesOfType<Home>()
                             .AtUri("/Home")
                             .HandledBy<HomeHandler>()
                             .RenderedByAspx("~/Views/HomeView.aspx");

                ResourceSpace.Has.ResourcesOfType<IEnumerable<Document>>()
                             .AtUri("/folders")
                             .HandledBy<FoldersHandler>()
                             .AsJsonDataContract();
            }
        }
    }
}