using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                ResourceSpace.Has.ResourcesOfType<Home>().AtUri("/home").HandledBy<HomeHandler>().RenderedByAspx("~/Views/HomeView.aspx");
            }
        }
    }
}