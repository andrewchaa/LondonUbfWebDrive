using System.Collections.Generic;
using System.Web.Http;
using LondonUbfWebDrive.Domain;
using LondonUbfWebDrive.Domain.Interfaces;
using LondonUbfWebDrive.Domain.Model;
using LondonUbfWebDrive.Domain.Services;

namespace LondonUbfWebDrive.Controllers
{
    public class BreadcrumbsController : ApiController
    {
        private readonly IBreadcrumbService _breadcrumbService;

        public BreadcrumbsController(IBreadcrumbService breadcrumbService)
        {
            _breadcrumbService = breadcrumbService;
        }

        // GET api/breadcrumbs
        public IEnumerable<Breadcrumb> Get()
        {
            return _breadcrumbService.ConvertFrom(string.Empty);
        }

        // GET api/breadcrumbs/5
        public IEnumerable<Breadcrumb> Get(string path)
        {
            return _breadcrumbService.ConvertFrom(path);
        }

    }
}
