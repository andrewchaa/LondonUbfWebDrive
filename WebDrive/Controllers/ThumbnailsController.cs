using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;

namespace WebDrive.Controllers
{
    public class ThumbnailsController : ApiController
    {
        // GET api/thumbnails
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/thumbnails/5
        public void Get(string path)
        {
//            var image = new WebImage(@"C:\Users\andrew\Documents\Projects\WebDrive\WebDrive\Images\Desert.jpg")
//                .Resize(100, 100, true, true)
//                .GetBytes();
//
//            return FileContentResult()

        }

        // POST api/thumbnails
        public void Post([FromBody]string value)
        {
        }

        // PUT api/thumbnails/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/thumbnails/5
        public void Delete(int id)
        {
        }
    }
}
