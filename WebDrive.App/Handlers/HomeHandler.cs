using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDrive.App.Resources;

namespace WebDrive.App.Handlers
{
    public class HomeHandler
    {
        public object Get()
        {
            return new Home {Title = "Web drive"};
        }
    }
}