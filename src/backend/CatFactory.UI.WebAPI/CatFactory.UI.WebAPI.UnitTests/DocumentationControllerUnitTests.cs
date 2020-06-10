using System.Threading.Tasks;
using CatFactory.ObjectRelationalMapping;
using CatFactory.UI.WebAPI.Controllers;
using CatFactory.UI.WebAPI.Models;
using CatFactory.UI.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CatFactory.UI.WebAPI.UnitTests
{
    public class DocumentationControllerUnitTests
    {
        public DocumentationControllerUnitTests()
        {
        }

        [Fact]
        public async Task TestGetImportedDatabases()
        {
            // Arrange
            var logger = LoggingHelper.GetLogger<DocumentationController>();
            var hostingEnvironment = HostingEnvironmentMocker.GetHostingEnvironment();
            var apiConfig = new ApiConfig();
            var dbService = new DbService(hostingEnvironment, apiConfig);
            var controller = new DocumentationController(logger, dbService);

            // Act
            var response = await controller.GetImportedDatabasesAsync() as ObjectResult;
            var value = response.Value as IListResponse<ImportedDatabase>;

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestImportDatabase()
        {
            // Arrange
            var logger = LoggingHelper.GetLogger<DocumentationController>();
            var hostingEnvironment = HostingEnvironmentMocker.GetHostingEnvironment();
            var apiConfig = new ApiConfig();
            var dbService = new DbService(hostingEnvironment, apiConfig);
            var controller = new DocumentationController(logger, dbService);
            var request = new ImportDatabaseRequest
            {
                Name = "OnlineStore",
                ConnectionString = "server=(local);database=OnlineStore;integrated security=yes;",
                ImportTables = true,
                ImportViews = true
            };

            // Act
            var response = await controller.ImportDatabaseAsync(request) as ObjectResult;
            var value = response.Value as ImportDatabaseResponse;

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestGetDatabaseDetailAsync()
        {
            // Arrange
            var logger = LoggingHelper.GetLogger<DocumentationController>();
            var hostingEnvironment = HostingEnvironmentMocker.GetHostingEnvironment();
            var apiConfig = new ApiConfig();
            var dbService = new DbService(hostingEnvironment, apiConfig);
            var controller = new DocumentationController(logger, dbService);
            var request = new DbRequest
            {
                Name = "OnlineStore"
            };

            // Act
            var response = await controller.GetDatabaseDetailAsync(request) as ObjectResult;
            var value = response.Value as ISingleResponse<DatabaseDetail>;

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestGetTableAsync()
        {
            // Arrange
            var logger = LoggingHelper.GetLogger<DocumentationController>();
            var hostingEnvironment = HostingEnvironmentMocker.GetHostingEnvironment();
            var apiConfig = new ApiConfig();
            var dbService = new DbService(hostingEnvironment, apiConfig);
            var controller = new DocumentationController(logger, dbService);
            var request = new DbRequest
            {
                Name = "OnlineStore",
                Table = "Sales.OrderHeader"
            };

            // Act
            var response = await controller.GetTableAsync(request) as ObjectResult;
            var value = response.Value as ISingleResponse<ITable>;

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestGetViewAsync()
        {
            // Arrange
            var hostingEnvironment = HostingEnvironmentMocker.GetHostingEnvironment();
            var apiConfig = new ApiConfig();
            var dbService = new DbService(hostingEnvironment, apiConfig);
            var logger = LoggingHelper.GetLogger<DocumentationController>();
            var controller = new DocumentationController(logger, dbService);
            var request = new DbRequest
            {
                Name = "OnlineStore",
                View = "Sales.OrderSummary"
            };

            // Act
            var response = await controller.GetViewAsync(request) as ObjectResult;
            var value = response.Value as ISingleResponse<IView>;

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestEditDescriptionAsync()
        {
            // Arrange
            var hostingEnvironment = HostingEnvironmentMocker.GetHostingEnvironment();
            var apiConfig = new ApiConfig();
            var dbService = new DbService(hostingEnvironment, apiConfig);
            var logger = LoggingHelper.GetLogger<DocumentationController>();
            var controller = new DocumentationController(logger, dbService);
            var request = new DbRequest
            {
                Name = "OnlineStore",
                Table = "Warehouse.Product",
                Type = "table",
                Description = "Products catalog (unit tests)"
            };

            // Act
            var response = await controller.EditDescriptionAsync(request) as ObjectResult;
            var value = response.Value as ISingleResponse<EditDescription>;

            // Assert
            Assert.False(value.DidError);
        }
    }
}
