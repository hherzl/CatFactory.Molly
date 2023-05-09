﻿using CatFactory.GUI.API.Filters;
using CatFactory.GUI.API.Models;
using CatFactory.GUI.API.Models.Common;
using CatFactory.GUI.API.Services;
using CatFactory.SqlServer;
using CatFactory.SqlServer.DatabaseObjectModel.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CatFactory.GUI.API.Controllers
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

            var response = new ListResponse<DatabaseItemModel>(databases);

            return response.ToOkResult();
        }

        [HttpGet("database/{id}")]
        [ProducesResponseType(200, Type = typeof(ISingleResponse<DatabaseDetailsModel>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetDatabaseAsync(string id)
        {
            var database = await _codeFactoryService.GetDatabaseDetailsAsync(id);

            if (database == null)
                return NotFound();

            var response = new SingleResponse<DatabaseDetailsModel>(database);

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

            var model = new TableDetailsModel
            {
                FullName = table.FullName,
                Schema = table.Schema,
                Name = table.Name,
                Description = table.Description,
                Identity = new IdentityDetailsModel(table.Identity),
                Columns = table.Columns.Select(item => new ColumnItemModel(item)).ToList(),
                PrimaryKey = new PrimaryKeyDetailsModel(table.PrimaryKey),
                ForeignKeys = table.ForeignKeys?.Select(item => new ForeignKeyItemModel(item)).ToList(),
                Uniques = table.Uniques?.Select(item => new UniqueItemModel(item)).ToList(),
                Checks = table.Checks?.Select(item => new CheckItemModel(item)).ToList(),
                Defaults = table.Defaults?.Select(item => new DefaultItemModel(item)).ToList(),
                Indexes = table.Indexes?.Select(item => new IndexItemModel(item)).ToList()
            };

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

            var model = new ViewDetailsModel
            {
                FullName = view.FullName,
                Schema = view.Schema,
                Name = view.Name,
                Description = view.Description,
                Identity = new IdentityDetailsModel(view.Identity),
                Columns = view.Columns.Select(item => new ColumnItemModel(item)).ToList(),
                Indexes = view.Indexes?.Select(item => new IndexItemModel(item)).ToList()
            };

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
