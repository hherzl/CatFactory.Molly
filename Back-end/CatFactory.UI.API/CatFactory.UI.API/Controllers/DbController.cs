using System;
using System.Linq;
using System.Threading.Tasks;
using CatFactory.Mapping;
using CatFactory.SqlServer;
using CatFactory.UI.API.Models;
using CatFactory.UI.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CatFactory.UI.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class DbController : Controller
    {
        private ILogger Logger;
        private DbService DbService;

        public DbController(ILogger<DbController> logger, DbService dbService)
        {
            Logger = logger;
            DbService = dbService;
        }

        [HttpGet("ImportedDatabases")]
        public async Task<IActionResult> GetImportedDatabasesAsync()
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetImportedDatabasesAsync));

            var response = new ListResponse<ImportedDatabase>();

            try
            {
                response.Model = await DbService.GetImportedDatabasesAsync();
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response.ToHttpResponse();
        }

        [HttpPost("ImportDatabase")]
        public async Task<IActionResult> ImportDatabaseAsync([FromBody] ImportDatabaseRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(ImportDatabaseAsync));

            var response = new ImportDatabaseResponse();

            try
            {
                var databaseFactory = new SqlServerDatabaseFactory
                {
                    ImportSettings = new DatabaseImportSettings
                    {
                        Name = request.Name,
                        ConnectionString = request.ConnectionString,
                        ImportTables = request.ImportTables,
                        ImportViews = request.ImportViews,
                        ExtendedProperties = { Tokens.MsDescription }
                    }
                };

                var db = databaseFactory.Import();

                await DbService.SerializeAsync(databaseFactory.ImportSettings);

                await DbService.SerializeAsync(db);

                response.Message = "The database was imported successfully";
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response.ToHttpResponse();
        }

        [HttpPost("DatabaseDetail")]
        public async Task<IActionResult> GetDatabaseDetailAsync([FromBody] DbRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetDatabaseDetailAsync));

            var response = new SingleResponse<DatabaseDetail>();

            try
            {
                response.Model = await DbService.GetDatabaseDetailAsync(request.Name);
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response.ToHttpResponse();
        }

        [HttpPost("Table")]
        public async Task<IActionResult> GetTableAsync([FromBody] DbRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetTableAsync));

            var response = new SingleResponse<ITable>();

            try
            {
                response.Model = await DbService.GetTableAsync(request.Name, request.Table);
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response.ToHttpResponse();
        }

        [HttpPost("View")]
        public async Task<IActionResult> GetViewAsync([FromBody] DbRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetViewAsync));

            var response = new SingleResponse<IView>();

            try
            {
                response.Model = await DbService.GetViewAsync(request.Name, request.View);
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response.ToHttpResponse();
        }

        [HttpPost("EditDescription")]
        public async Task<IActionResult> EditDescriptionAsync([FromBody] DbRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(EditDescriptionAsync));

            var response = new SingleResponse<EditDescription>();

            try
            {
                var db = await DbService.GetDatabaseAsync(request.Name);

                var databaseFactory = new SqlServerDatabaseFactory
                {
                    ImportSettings = await DbService.GetDatabaseImportSettingsAsync(request.Name)
                };

                if (request.Type == Tokens.Table)
                {
                    var table = db.FindTable(request.Table);

                    if (string.IsNullOrEmpty(request.Column))
                    {
                        databaseFactory.AddOrUpdateExtendedProperty(table, Tokens.MsDescription, request.FixedDescription);
                        table.Description = request.Description;
                    }
                    else
                    {
                        var column = table.Columns.First(item => item.Name == request.Column);
                        databaseFactory.AddOrUpdateExtendedProperty(table, column, Tokens.MsDescription, request.FixedDescription);
                        column.Description = request.Description;
                    }
                }
                else if (request.Type == Tokens.View)
                {
                    var view = db.FindView(request.View);

                    if (string.IsNullOrEmpty(request.Column))
                    {
                        databaseFactory.AddOrUpdateExtendedProperty(view, Tokens.MsDescription, request.FixedDescription);
                        view.Description = request.Description;
                    }
                    else
                    {
                        var column = view.Columns.First(item => item.Name == request.Column);
                        databaseFactory.AddOrUpdateExtendedProperty(view, view.Columns.First(item => item.Name == request.Column), Tokens.MsDescription, request.FixedDescription);
                        column.Description = request.Description;
                    }
                }

                await DbService.SerializeAsync(db);

                await DbService.SerializeAsync(databaseFactory.ImportSettings);
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response.ToHttpResponse();
        }
    }
}
