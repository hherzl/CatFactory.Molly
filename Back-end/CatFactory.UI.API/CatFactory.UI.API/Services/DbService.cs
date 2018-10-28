using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatFactory.ObjectRelationalMapping;
using CatFactory.SqlServer;
using CatFactory.UI.API.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace CatFactory.UI.API.Services
{
    public class DbService
    {
        private IHostingEnvironment HostingEnvironment;
        private ApiConfig ApiConfig;

        public DbService(IHostingEnvironment hostingEnvironment, ApiConfig apiConfig)
        {
            HostingEnvironment = hostingEnvironment;
            ApiConfig = apiConfig;
        }

        string DatabasesDirectoryName
            => Path.Combine(HostingEnvironment.ContentRootPath, ApiConfig.DatabasesDirectoryName);

        public string GetDbFileName(string name)
            => Path.Combine(DatabasesDirectoryName, name);

        string DatabaseImportSettingsDirectoryName
            => Path.Combine(HostingEnvironment.ContentRootPath, ApiConfig.DatabaseImportSettingsName);

        public string GetDatabaseImportSettingsName(string name)
            => Path.Combine(DatabaseImportSettingsDirectoryName, name);

        public async Task<IEnumerable<ImportedDatabase>> GetImportedDatabasesAsync()
        {
            if (!Directory.Exists(DatabasesDirectoryName))
                Directory.CreateDirectory(DatabasesDirectoryName);

            var files = Directory.GetFiles(DatabasesDirectoryName);

            var result = new List<ImportedDatabase>();

            foreach (var item in files)
            {
                var objectInString = await File.ReadAllTextAsync(item, Encoding.Default);

                var db = JsonConvert.DeserializeObject<Database>(objectInString);

                result.Add(new ImportedDatabase
                {
                    Name = db.Name,
                    TablesCount = db.Tables.Count,
                    ViewsCount = db.Views.Count
                });
            }

            return result;
        }

        public async Task<Database> GetDatabaseAsync(string name)
        {
            if (!Directory.Exists(DatabasesDirectoryName))
                Directory.CreateDirectory(DatabasesDirectoryName);

            var objectInString = await File.ReadAllTextAsync(GetDbFileName(name), Encoding.Default);

            return JsonConvert.DeserializeObject<Database>(objectInString);
        }

        public async Task SerializeAsync(DatabaseImportSettings dbImportSettings)
        {
            if (!Directory.Exists(DatabaseImportSettingsDirectoryName))
                Directory.CreateDirectory(DatabaseImportSettingsDirectoryName);

            var objectInString = JsonConvert.SerializeObject(dbImportSettings, Formatting.Indented);

            await File.WriteAllTextAsync(GetDatabaseImportSettingsName(dbImportSettings.Name), objectInString, Encoding.Default);
        }

        public async Task SerializeAsync(Database db)
        {
            if (!Directory.Exists(DatabasesDirectoryName))
                Directory.CreateDirectory(DatabasesDirectoryName);

            var objectInString = JsonConvert.SerializeObject(db, Formatting.Indented);

            await File.WriteAllTextAsync(GetDbFileName(db.Name), objectInString, Encoding.Default);
        }

        public async Task<DatabaseDetail> GetDatabaseDetailAsync(string name)
        {
            var objectInString = await File.ReadAllTextAsync(GetDbFileName(name), Encoding.Default);

            var db = JsonConvert.DeserializeObject<Database>(objectInString);

            return new DatabaseDetail
            {
                Name = db.Name,
                Tables = db.Tables.Select(item => new TableDetail
                {
                    Schema = item.Schema,
                    Name = item.Name,
                    Type = item.Type,
                    FullName = item.FullName,
                    ColumnsCount = item.Columns.Count,
                    PrimaryKey = item.PrimaryKey == null ? "" : string.Join(",", item.PrimaryKey.Key),
                    Identity = item.Identity == null ? "" : item.Identity.Name
                }).ToList(),
                Views = db.Views.Select(item => new ViewDetail
                {
                    Schema = item.Schema,
                    Name = item.Name,
                    Type = item.Type,
                    FullName = item.FullName,
                    ColumnsCount = item.Columns.Count,
                    Identity = item.Identity == null ? "" : item.Identity.Name
                }).ToList(),
                DatabaseTypeMaps = db.DatabaseTypeMaps
            };
        }

        public async Task<DatabaseImportSettings> GetDatabaseImportSettingsAsync(string dbName)
        {
            var objectInString = await File.ReadAllTextAsync(GetDatabaseImportSettingsName(dbName), Encoding.Default);

            var dbImportSettings = JsonConvert.DeserializeObject<DatabaseImportSettings>(objectInString);

            return dbImportSettings;
        }

        public async Task<ITable> GetTableAsync(string dbName, string table)
        {
            var objectInString = await File.ReadAllTextAsync(GetDbFileName(dbName), Encoding.Default);

            var db = JsonConvert.DeserializeObject<Database>(objectInString);

            return db.FindTable(table);
        }

        public async Task<IView> GetViewAsync(string dbName, string view)
        {
            var objectInString = await File.ReadAllTextAsync(GetDbFileName(dbName), Encoding.Default);

            var db = JsonConvert.DeserializeObject<Database>(objectInString);

            return db.FindView(view);
        }
    }
}
