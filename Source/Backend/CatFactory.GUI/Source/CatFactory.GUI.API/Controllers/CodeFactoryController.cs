using Microsoft.AspNetCore.Mvc;

namespace CatFactory.GUI.API.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class CodeFactoryController : ControllerBase
    {
        private readonly ILogger<CodeFactoryController> _logger;

        public CodeFactoryController(ILogger<CodeFactoryController> logger)
        {
            _logger = logger;
        }

        [HttpPost("import-database")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult ImportDatabase()
        {
            return Ok();
        }

        [HttpGet("database")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult GetDatabases()
        {
            return Ok();
        }

        [HttpGet("database/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult GetDatabase(string id)
        {
            return Ok();
        }
    }
}
