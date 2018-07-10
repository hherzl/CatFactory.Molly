using System.Threading.Tasks;
using CatFactory.Mapping;
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
        }

        [Fact]
        public async Task TestGetImportedDatabases()
        {
            // Arrange
            var logger = LoggerMocker.GetLogger<DbController>();
            var hostingEnvironment = new MockHostingEnvironment();
            var apiConfig = new ApiConfig();
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
            var logger = LoggerMocker.GetLogger<DbController>();
            var hostingEnvironment = new MockHostingEnvironment();
            var apiConfig = new ApiConfig();
            var dbService = new DbService(hostingEnvironment, apiConfig);
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
            var logger = LoggerMocker.GetLogger<DbController>();
            var hostingEnvironment = new MockHostingEnvironment();
            var apiConfig = new ApiConfig();
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
            var logger = LoggerMocker.GetLogger<DbController>();
            var hostingEnvironment = new MockHostingEnvironment();
            var apiConfig = new ApiConfig();
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
            var logger = LoggerMocker.GetLogger<DbController>();
            var hostingEnvironment = new MockHostingEnvironment();
            var apiConfig = new ApiConfig();
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
            var logger = LoggerMocker.GetLogger<DbController>();
            var hostingEnvironment = new MockHostingEnvironment();
            var apiConfig = new ApiConfig();
            var dbService = new DbService(hostingEnvironment, apiConfig);
            var controller = new DbController(logger, dbService);
            var request = new DbRequest
            {
                Name = "Store",
                Table = "Production.Product",
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
