using CatFactory.GUI.API.Models;
using CatFactory.GUI.API.Models.Common;
using CatFactory.GUI.API.Services;
using CatFactory.ObjectRelationalMapping;
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
            var databaseFactory = new SqlServerDatabaseFactory
            {
                DatabaseImportSettings = new DatabaseImportSettings
                {
                    Name = request.Name,
                    ConnectionString = request.ConnectionString,
                    ImportTables = request.ImportTables,
                    ImportViews = request.ImportViews,
                    ExtendedProperties =
                    {
                        Tokens.MS_DESCRIPTION
                    }
                }
            };

            var database = await databaseFactory.ImportAsync();

            await _codeFactoryService.SerializeAsync(databaseFactory.DatabaseImportSettings);

            await _codeFactoryService.SerializeAsync(database);

            return Ok();
        }

        [HttpGet("database")]
        [ProducesResponseType(200, Type = typeof(IListResponse<DatabaseItemModel>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetDatabasesAsync()
        {
            var databases = await _codeFactoryService.GetDatabasesAsync();

            var response = new ListResponse<DatabaseItemModel>(databases);

            return Ok(response);
        }

        [HttpGet("database/{id}")]
        [ProducesResponseType(200, Type = typeof(ISingleResponse<DatabaseDetailsModel>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetDatabaseAsync(string id)
        {
            var database = await _codeFactoryService.GetDatabaseAsync(id);

            if (database == null)
                return NotFound();

            var response = new SingleResponse<DatabaseDetailsModel>(database);

            return Ok(response);
        }

        [HttpGet("database/{databaseName}/table/{tableName}")]
        [ProducesResponseType(200, Type = typeof(ISingleResponse<Table>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetTableAsync(string databaseName, string tableName)
        {
            var table = await _codeFactoryService.GetTableAsync(databaseName, tableName);

            if (table == null)
                return NotFound();

            var response = new SingleResponse<Table>(table);

            return Ok(response);
        }

        [HttpGet("database/{databaseName}/view/{viewName}")]
        [ProducesResponseType(200, Type = typeof(ISingleResponse<View>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetViewAsync(string databaseName, string viewName)
        {
            var view = await _codeFactoryService.GetViewAsync(databaseName, viewName);

            if (view == null)
                return NotFound();

            var response = new SingleResponse<View>(view);

            return Ok(response);
        }
    }
}
