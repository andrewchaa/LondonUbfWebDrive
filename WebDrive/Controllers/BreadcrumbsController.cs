using System.Collections.Generic;
using System.Web.Http;
using LondonUbfWebDrive.Domain;
using LondonUbfWebDrive.Domain.Interfaces;

namespace LondonUbfWebDrive.Controllers
{
    public class BreadcrumbsController : ApiController
    {
        private readonly IBreadcrumbMaker _breadcrumbMaker;

        public BreadcrumbsController(IBreadcrumbMaker breadcrumbMaker)
        {
            _breadcrumbMaker = breadcrumbMaker;
        }

        // GET api/breadcrumbs/5
        public IEnumerable<Breadcrumb> Get()
        {
            return _breadcrumbMaker.Make(string.Empty);
        }

        // GET api/breadcrumbs/5
        public IEnumerable<Breadcrumb> Get(string path)
        {
            return _breadcrumbMaker.Make(path);
        }

    }
}
