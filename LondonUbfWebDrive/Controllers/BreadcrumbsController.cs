using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LondonUbfWebDrive.Domain;
using LondonUbfWebDrive.Repositories;

namespace LondonUbfWebDrive.Controllers
{
    public class BreadcrumbsController : ApiController
    {
        private readonly IBreadcrumbRepository _repository;

        public BreadcrumbsController(IBreadcrumbRepository repository)
        {
            _repository = repository;
        }

        // GET api/breadcrumbs/5
        public IEnumerable<Breadcrumb> Get()
        {
            return _repository.List("\\");
        }

        // GET api/breadcrumbs/5
        public IEnumerable<Breadcrumb> Get(string path)
        {
            return _repository.List(path);
        }

    }
}
