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

            var response = new Response();

            var database = (SqlServerDatabase)await databaseFactory.ImportAsync();

            database.SyncMsDescription();

            await _codeFactoryService.SerializeAsync(databaseFactory.DatabaseImportSettings);

            await _codeFactoryService.SerializeAsync(database);

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

        [HttpPost("edit-description")]
        [ProducesResponseType(200, Type = typeof(IResponse))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> EditDescriptionAsync([FromBody] EditDescriptionRequest request)
        {
            var response = new Response();

            if (!request.HasDescription)
                return response.ToOkResult();

            var database = await _codeFactoryService.GetDatabaseAsync(request.Database);

            var databaseFactory = new SqlServerDatabaseFactory(await _codeFactoryService.GetDatabaseImportSettingsAsync(request.Database));

            var connection = databaseFactory.GetConnection();

            if (request.IsTable)
            {
                var table = database.FindTable(request.Table);

                if (request.IsColumn)
                {
                    await connection.DropExtendedPropertyIfExistsAsync(table, table[request.Column], SqlServerToken.MS_DESCRIPTION);

                    await connection.AddExtendedPropertyAsync(table, table[request.Column], SqlServerToken.MS_DESCRIPTION, request.FixedDescription);
                }
                else
                {
                    await connection.DropExtendedPropertyIfExistsAsync(table, SqlServerToken.MS_DESCRIPTION);

                    await connection.AddExtendedPropertyAsync(table, SqlServerToken.MS_DESCRIPTION, request.FixedDescription);

                    table.Description = request.Description;
                }
            }
            else if (request.IsView)
            {
                var view = database.FindView(request.View);

                if (request.IsColumn)
                {
                    await connection.DropExtendedPropertyIfExistsAsync(view, view[request.Column], SqlServerToken.MS_DESCRIPTION);

                    await connection.AddExtendedPropertyAsync(view, view[request.Column], SqlServerToken.MS_DESCRIPTION, request.FixedDescription);
                }
                else
                {
                    await connection.DropExtendedPropertyIfExistsAsync(view, SqlServerToken.MS_DESCRIPTION);

                    await connection.AddExtendedPropertyAsync(view, SqlServerToken.MS_DESCRIPTION, request.FixedDescription);

                    view.Description = request.Description;
                }
            }
            else
            {
                await connection.DropExtendedPropertyIfExistsAsync(SqlServerToken.MS_DESCRIPTION);

                await connection.AddExtendedPropertyAsync(SqlServerToken.MS_DESCRIPTION, request.FixedDescription);
            }

            await _codeFactoryService.SerializeAsync(databaseFactory.DatabaseImportSettings);

            await _codeFactoryService.SerializeAsync(database);

            response.Message = "The description was updated successfully";

            return response.ToOkResult();
        }
    }
}
