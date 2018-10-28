using System.Threading.Tasks;
using CatFactory.ObjectRelationalMapping;
using CatFactory.UI.API.Controllers;
using CatFactory.UI.API.Models;
using CatFactory.UI.API.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CatFactory.UI.API.UnitTests
{
    public class DbControllerUnitTests
    {
        public DbControllerUnitTests()
        {
            // todo: Create files for import settings and database
        }

        [Fact]
        public async Task TestGetImportedDatabases()
        {
            // Arrange
            var hostingEnvironment = HostingEnvironmentMocker.GetMockHostingEnvironment();
            var apiConfig = new ApiConfig();
            var logger = LoggerMocker.GetLogger<DbController>();
            var dbService = new DbService(hostingEnvironment, apiConfig);
            var controller = new DbController(logger, dbService);

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
            var hostingEnvironment = HostingEnvironmentMocker.GetMockHostingEnvironment();
            var apiConfig = new ApiConfig();
            var dbService = new DbService(hostingEnvironment, apiConfig);
            var logger = LoggerMocker.GetLogger<DbController>();
            var controller = new DbController(logger, dbService);
            var request = new ImportDatabaseRequest
            {
                Name = "Store",
                ConnectionString = "server=(local);database=Store;integrated security=yes;",
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
            var hostingEnvironment = HostingEnvironmentMocker.GetMockHostingEnvironment();
            var apiConfig = new ApiConfig();
            var logger = LoggerMocker.GetLogger<DbController>();
            var dbService = new DbService(hostingEnvironment, apiConfig);
            var controller = new DbController(logger, dbService);
            var request = new DbRequest
            {
                Name = "Store"
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
            var hostingEnvironment = HostingEnvironmentMocker.GetMockHostingEnvironment();
            var apiConfig = new ApiConfig();
            var logger = LoggerMocker.GetLogger<DbController>();
            var dbService = new DbService(hostingEnvironment, apiConfig);
            var controller = new DbController(logger, dbService);
            var request = new DbRequest
            {
                Name = "Store",
                Table = "Sales.Order"
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
            var hostingEnvironment = HostingEnvironmentMocker.GetMockHostingEnvironment();
            var apiConfig = new ApiConfig();
            var logger = LoggerMocker.GetLogger<DbController>();
            var dbService = new DbService(hostingEnvironment, apiConfig);
            var controller = new DbController(logger, dbService);
            var request = new DbRequest
            {
                Name = "Store",
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
            var hostingEnvironment = HostingEnvironmentMocker.GetMockHostingEnvironment();
            var apiConfig = new ApiConfig();
            var logger = LoggerMocker.GetLogger<DbController>();
            var dbService = new DbService(hostingEnvironment, apiConfig);
            var controller = new DbController(logger, dbService);
            var request = new DbRequest
            {
                Name = "Store",
                Table = "Production.Product",
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
