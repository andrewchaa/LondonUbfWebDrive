using System;
using System.Collections.Generic;
using System.Web.Http;
using LondonUbfWebDrive.Domain;
using LondonUbfWebDrive.Domain.Interfaces;
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
            var documentMetadatas = _metaDataRepository.List();
            return documentMetadatas;
        }

        // GET api/metadata/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/metadata
        public void Post([FromBody] string value)
        {
            var document = JsonConvert.DeserializeObject<Document>(value);
            var metadata = new DocumentMetadata {Name = document.Name, FullName = document.FullName, DownloadTime = DateTime.UtcNow };
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
