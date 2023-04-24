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
    }
}
