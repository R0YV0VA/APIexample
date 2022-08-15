using ExtensionsCRUD.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExtensionsCRUD.Rest.Controllers
{
    [Route("api/extensions")]
    [ApiController]
    public class ExtensionController : Controller
    {
        readonly IExtensionService _service;
        private readonly IConfiguration _configuration;
        public ExtensionController(IExtensionService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        [HttpPost]
        public ExtensionEntity createExtensionData([FromBody] ExtensionPost extension)
        {
            var result = _service.createExtension(extension.name, extension.description, extension.version, _configuration.GetConnectionString("PostgreSQLConnectionString"));
            return new ExtensionEntity(result.Id, result.Name, result.Description, result.Version);
        }

        [HttpGet("{id}")]
        public ExtensionEntity getExtensionById(int id)
        {
            var result = _service.getExtensionById(id, _configuration.GetConnectionString("PostgreSQLConnectionString"));
            return new ExtensionEntity(result.Id, result.Name, result.Description, result.Version);
        }

        [HttpGet]
        public List<ExtensionEntity> getAllExtensions()
        {
            var extensions = _service.getExtensions(_configuration.GetConnectionString("PostgreSQLConnectionString"));
            var result = new List<ExtensionEntity>();
            foreach(var extension in extensions)
            {
                result.Add(new ExtensionEntity(extension.Id, extension.Name, extension.Description, extension.Version));
            }
            return result;
        }

        [HttpPut]
        public ExtensionEntity putExtensionData([FromBody] ExtensionEntity extension)
        {
            var result = _service.putExtension(extension.Id, extension.Name, extension.Description, extension.Version, _configuration.GetConnectionString("PostgreSQLConnectionString"));
            return new ExtensionEntity(result.Id, result.Name, result.Description, result.Version);
        }

        [HttpDelete("{id}")]
        public ExtensionEntity deleteExtensionById(int id)
        {
            var result = _service.deleteExtensionById(id, _configuration.GetConnectionString("PostgreSQLConnectionString"));
            return new ExtensionEntity(result.Id, result.Name, result.Description, result.Version);
        }
    }
}
