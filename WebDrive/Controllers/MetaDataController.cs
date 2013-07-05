using System;
using System.Collections.Generic;
using System.Web.Http;
using LondonUbfWebDrive.Domain.Interfaces;
using LondonUbfWebDrive.Domain.Model;
using LondonUbfWebDrive.Models;
using Newtonsoft.Json;

namespace LondonUbfWebDrive.Controllers
{
    public class MetaDataController : ApiController
    {
        private readonly IMetaDataRepository _metaDataRepository;

        public MetaDataController(IMetaDataRepository metaDataRepository)
        {
            _metaDataRepository = metaDataRepository;
        }

        // GET api/metadata
        public IEnumerable<DocumentMetadata> Get()
        {
            return _metaDataRepository.List();
        }

        // GET api/metadata/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/metadata
        public void Post([FromBody] string value)
        {
            var documentViewModel = JsonConvert.DeserializeObject<DocumentViewModel>(value);
            var metadata = new DocumentMetadata
                {
                    Name = documentViewModel.Name, 
                    FullName = documentViewModel.FullName, 
                    DownloadTime = DateTime.UtcNow
                };

            _metaDataRepository.Save(metadata);
        }

        // PUT api/metadata/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/metadata/5
        public void Delete(int id)
        {
        }
    }
}
