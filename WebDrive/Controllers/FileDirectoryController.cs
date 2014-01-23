using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
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
            var entities = GetWebEntities(_config.PictureDirectory);

            return entities;
        }

        private IEnumerable<WebEntity> GetWebEntities(string path)
        {
            var directories = _fileDirectoryService.EnumerateDirectories(path);
            var files = _fileDirectoryService.EnumerateFiles(path);

            var entities = new List<WebEntity>();
            entities.AddRange(directories);
            entities.AddRange(files);
            return entities;
        }

        // GET api/filedirectory/5
        public IEnumerable<WebEntity> Get(string path)
        {
            string decodedPath = Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(path));

            return GetWebEntities(decodedPath);
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
