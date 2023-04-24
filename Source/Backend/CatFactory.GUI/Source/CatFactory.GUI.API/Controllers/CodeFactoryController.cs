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

            request.Name = "RothschildHouse";
            request.ConnectionString = "server=(local); database=RothschildHouse; integrated security=yes; TrustServerCertificate=True;";
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

            var database = await databaseFactory.ImportAsync();

            await _codeFactoryService.SerializeAsync(databaseFactory.DatabaseImportSettings);

            await _codeFactoryService.SerializeAsync(database);

            return Ok();
        }

        [HttpGet("database")]
        [ProducesResponseType(200, Type = typeof(IListResponse<ImportedDatabase>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetDatabasesAsync()
        {
            var databases = await _codeFactoryService.GetImportedDatabasesAsync();

            var response = new ListResponse<ImportedDatabase>(databases);

            return Ok(response);
        }

        [HttpGet("database/{id}")]
        [ProducesResponseType(200, Type = typeof(ISingleResponse<DatabaseDetails>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetDatabaseAsync(string id)
        {
            var database = await _codeFactoryService.GetDatabaseDetailsAsync(id);

            if (database == null)
                return NotFound();

            var response = new SingleResponse<DatabaseDetails>(database);

            return Ok(response);
        }
    }
}
