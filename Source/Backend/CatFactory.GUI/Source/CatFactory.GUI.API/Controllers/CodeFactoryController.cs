using CatFactory.GUI.API.Models;
using CatFactory.GUI.API.Models.Common;
using CatFactory.GUI.API.Services;
using CatFactory.SqlServer;
using Microsoft.AspNetCore.Mvc;

namespace CatFactory.GUI.API.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class CodeFactoryController : ControllerBase
    {
        private readonly ILogger<CodeFactoryController> _logger;
        private readonly CodeFactoryService _codeFactoryService;

        public CodeFactoryController(ILogger<CodeFactoryController> logger, CodeFactoryService codeFactoryService)
        {
            _logger = logger;
            _codeFactoryService = codeFactoryService;
        }

        [HttpPost("import-database")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ImportDatabaseAsync([FromBody] ImportDatabaseRequest request)
        {
            // todo: Add testing and remove this code

            request = new ImportDatabaseRequest();

            request.Name = "Northwind";
            request.ConnectionString = "server=(local); database=Northwind; integrated security=yes; TrustServerCertificate=True;";
            request.ImportTables = true;
            request.ImportViews = true;

            var databaseFactory = new SqlServerDatabaseFactory
            {
                DatabaseImportSettings = new DatabaseImportSettings
                {
                    Name = request.Name,
                    ConnectionString = request.ConnectionString,
                    ImportTables = request.ImportTables,
                    ImportViews = request.ImportViews,
                    ExtendedProperties = { Tokens.MS_DESCRIPTION }
                }
            };

            var db = await databaseFactory.ImportAsync();

            await _codeFactoryService.SerializeAsync(databaseFactory.DatabaseImportSettings);

            await _codeFactoryService.SerializeAsync(db);

            return Ok();
        }

        [HttpGet("database")]
        [ProducesResponseType(200, Type = typeof(IListResponse<ImportedDatabase>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetDatabasesAsync()
        {
            var response = new ListResponse<ImportedDatabase>(await _codeFactoryService.GetImportedDatabasesAsync());

            return Ok(response);
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
