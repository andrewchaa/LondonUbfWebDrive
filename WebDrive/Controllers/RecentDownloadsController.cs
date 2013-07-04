using System.Collections.Generic;
using System.Web.Http;
using LondonUbfWebDrive.Domain;
using LondonUbfWebDrive.Domain.Interfaces;
using LondonUbfWebDrive.Domain.Model;

namespace LondonUbfWebDrive.Controllers
{
    public class RecentDownloadsController : ApiController
    {
        private readonly IMetaDataRepository _repository;

        public RecentDownloadsController(IMetaDataRepository repository)
        {
            _repository = repository;
        }

        // GET api/recentdownloads
        public IEnumerable<DocumentMetadata> Get()
        {
            return _repository.ListRecentDownloads();
        }

        // GET api/recentdownloads/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/recentdownloads
        public void Post([FromBody]string value)
        {
        }

        // PUT api/recentdownloads/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/recentdownloads/5
        public void Delete(int id)
        {
        }
    }
}
