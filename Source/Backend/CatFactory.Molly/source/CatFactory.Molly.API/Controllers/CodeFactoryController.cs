using CatFactory.Molly.API.Filters;
using CatFactory.Molly.API.Models;
using CatFactory.Molly.API.Models.Common;
using CatFactory.Molly.API.Services;
using CatFactory.SqlServer;
using CatFactory.SqlServer.DatabaseObjectModel.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CatFactory.Molly.API.Controllers
{
    [ApiController]
    [Route("api/v1")]
    [TypeFilter(typeof(MollyExceptionFilter))]
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
        [ProducesResponseType(200, Type = typeof(IResponse))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ImportDatabaseAsync([FromBody] ImportDatabaseRequest request)
        {
            var databaseFactory = new SqlServerDatabaseFactory(
                DatabaseImportSettings.Create(request.Name, request.ConnectionString, request.ImportTables, request.ImportViews, SqlServerToken.MS_DESCRIPTION)
            );

            databaseFactory.DatabaseImportSettings.Name = request.Name;

            var database = (SqlServerDatabase)await databaseFactory.ImportAsync();

            database.SyncMsDescription();

            await _codeFactoryService.SerializeAsync(databaseFactory.DatabaseImportSettings);

            await _codeFactoryService.SerializeAsync(database);

            var response = new Response("The database was imported successfully");

            return response.ToOkResult();
        }

        [HttpGet("database")]
        [ProducesResponseType(200, Type = typeof(IListResponse<DatabaseItemModel>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetDatabasesAsync()
        {
            var databases = await _codeFactoryService.GetDatabasesAsync();

            var response = new ListResponse<DatabaseItemModel>();

            foreach (var item in databases)
            {
                response.Model.Add(new DatabaseItemModel(item.Name, item.Dbms, item.Tables.Count, item.Views.Count));
            }

            return response.ToOkResult();
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

            var model = new DatabaseDetailsModel(database);

            var response = new SingleResponse<DatabaseDetailsModel>(model);

            return response.ToOkResult();
        }

        [HttpGet("database/{databaseName}/table/{tableName}")]
        [ProducesResponseType(200, Type = typeof(ISingleResponse<TableDetailsModel>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetTableAsync(string databaseName, string tableName)
        {
            var table = await _codeFactoryService.GetTableAsync(databaseName, tableName);

            if (table == null)
                return NotFound();

            var model = new TableDetailsModel(table);

            var response = new SingleResponse<TableDetailsModel>(model);

            return response.ToOkResult();
        }

        [HttpGet("database/{databaseName}/view/{viewName}")]
        [ProducesResponseType(200, Type = typeof(ISingleResponse<ViewDetailsModel>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetViewAsync(string databaseName, string viewName)
        {
            var view = await _codeFactoryService.GetViewAsync(databaseName, viewName);

            if (view == null)
                return NotFound();

            var model = new ViewDetailsModel(view);

            var response = new SingleResponse<ViewDetailsModel>(model);

            return response.ToOkResult();
        }

        [HttpPut("database/{databaseName}/update-description")]
        [ProducesResponseType(200, Type = typeof(IResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateDatabaseDescriptionAsync(string databaseName, [FromBody] UpdateDescriptionRequest request)
        {
            _logger?.LogDebug($"'{nameof(UpdateDatabaseDescriptionAsync)}' has been invoked");

            var databaseImportSettings = await _codeFactoryService.GetDatabaseImportSettingsAsync(databaseName);
            using var connection = databaseImportSettings.GetConnection();

            _logger?.LogInformation($"Updating description for '{databaseName}' database...");

            await connection.DropExtendedPropertyIfExistsAsync(SqlServerToken.MS_DESCRIPTION);

            await connection.AddExtendedPropertyAsync(SqlServerToken.MS_DESCRIPTION, request.FixedDescription);

            _logger?.LogInformation($"The description for '{databaseName}' database was successfully...");

            var database = await _codeFactoryService.GetDatabaseAsync(databaseName);

            database.Description = request.FixedDescription;

            await _codeFactoryService.SerializeAsync(database);

            _logger?.LogInformation($"The local changes for '{databaseName}' database were saved successfully");

            var response = new Response("The description was updated successfully");

            return response.ToOkResult();
        }

        [HttpPut("database/{databaseName}/table/{tableName}/update-description")]
        [ProducesResponseType(200, Type = typeof(IResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateTableDescriptionAsync(string databaseName, string tableName, [FromBody] UpdateDescriptionRequest request)
        {
            _logger?.LogDebug($"'{nameof(UpdateTableDescriptionAsync)}' has been invoked");

            var databaseImportSettings = await _codeFactoryService.GetDatabaseImportSettingsAsync(databaseName);
            using var connection = databaseImportSettings.GetConnection();

            var database = await _codeFactoryService.GetDatabaseAsync(databaseName);
            var table = database.FindTable(tableName);

            await connection.DropExtendedPropertyIfExistsAsync(table, SqlServerToken.MS_DESCRIPTION);

            await connection.AddOrUpdateExtendedPropertyAsync(table, SqlServerToken.MS_DESCRIPTION, request.FixedDescription);

            table.Description = request.FixedDescription;

            await _codeFactoryService.SerializeAsync(database);

            _logger?.LogInformation($"The local changes for '{databaseName}' database were saved successfully");

            var response = new Response("The description was updated successfully");

            return response.ToOkResult();
        }

        [HttpPut("database/{databaseName}/table/{tableName}/column/{columnName}/update-description")]
        [ProducesResponseType(200, Type = typeof(IResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateTableColumnDescriptionAsync(string databaseName, string tableName, string columnName, [FromBody] UpdateDescriptionRequest request)
        {
            _logger?.LogDebug($"'{nameof(UpdateTableColumnDescriptionAsync)}' has been invoked");

            var databaseImportSettings = await _codeFactoryService.GetDatabaseImportSettingsAsync(databaseName);
            using var connection = databaseImportSettings.GetConnection();

            var database = await _codeFactoryService.GetDatabaseAsync(databaseName);
            var table = database.FindTable(tableName);
            var column = table[columnName];

            await connection.DropExtendedPropertyIfExistsAsync(table, column, SqlServerToken.MS_DESCRIPTION);

            await connection.AddOrUpdateExtendedPropertyAsync(table, column, SqlServerToken.MS_DESCRIPTION, request.FixedDescription);

            column.Description = request.FixedDescription;

            await _codeFactoryService.SerializeAsync(database);

            _logger?.LogInformation($"The local changes for '{databaseName}' database were saved successfully");

            var response = new Response("The description was updated successfully");

            return response.ToOkResult();
        }
    }
}
