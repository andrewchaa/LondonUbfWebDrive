using System;
using System.Collections.Generic;
using System.IO;
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
            var entities = GetWebEntities(string.Empty);

            return entities;
        }

        private IEnumerable<WebEntity> GetWebEntities(string relativePath)
        {
            string path = Path.Combine(_config.PictureDirectory, relativePath);
            var directories = _fileDirectoryService.EnumerateDirectories(path);
            var files = _fileDirectoryService.EnumerateFiles(path);

            var entities = new List<WebEntity>();
            entities.AddRange(directories);
            entities.AddRange(files);
            return entities;
        }

        // GET api/filedirectory/5
        public IEnumerable<WebEntity> Get(string id)
        {
            return GetWebEntities(id);
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
