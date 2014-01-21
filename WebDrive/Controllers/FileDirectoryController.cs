using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebDrive.Domain.Contracts;
using WebDrive.Domain.Model;

namespace WebDrive.Controllers
{
    public class FileDirectoryController : ApiController
    {
        private readonly IConfig _config;
        private readonly IFileDirectoryService _fileDirectoryService;

        public FileDirectoryController(IConfig config, IFileDirectoryService fileDirectoryService)
        {
            _config = config;
            _fileDirectoryService = fileDirectoryService;
        }

        // GET api/filedirectory
        public IEnumerable<WebEntity> Get()
        {
            var directories = _fileDirectoryService.EnumerateDirectories(_config.PictureDirectory);
            var files = _fileDirectoryService.EnumerateFiles(_config.PictureDirectory);

            var entities = new List<WebEntity>();
            entities.AddRange(directories);
            entities.AddRange(files);

            return entities;
        }

        // GET api/filedirectory/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/filedirectory
        public void Post([FromBody]string value)
        {
        }

        // PUT api/filedirectory/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/filedirectory/5
        public void Delete(int id)
        {
        }
    }
}
