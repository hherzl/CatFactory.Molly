using System.Text;
using System.Text.Json;
using CatFactory.GUI.API.Models;
using CatFactory.ObjectRelationalMapping;
using CatFactory.SqlServer;

namespace CatFactory.GUI.API.Services
{
    public class CodeFactoryService
    {
        static JsonSerializerOptions DefaultJsonSerializerOptions
            => new()
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };

        private readonly IWebHostEnvironment _webHostingEnvironment;
        private readonly GUISettings _guiSettings;

        public CodeFactoryService(IWebHostEnvironment webHostingEnvironment, GUISettings guiSettings)
        {
            _webHostingEnvironment = webHostingEnvironment;
            _guiSettings = guiSettings;
        }

        string DatabasesDirectoryName
            => Path.Combine(_webHostingEnvironment.ContentRootPath, _guiSettings.DatabasesDirectoryName);

        public string GetDbFileName(string name)
            => Path.Combine(DatabasesDirectoryName, name);

        string DatabaseImportSettingsDirectoryName
            => Path.Combine(_webHostingEnvironment.ContentRootPath, _guiSettings.DatabaseImportSettingsName);

        public string GetDatabaseImportSettingsName(string name)
            => Path.Combine(DatabaseImportSettingsDirectoryName, name);

        public async Task SerializeAsync(DatabaseImportSettings dbImportSettings)
        {
            if (!Directory.Exists(DatabaseImportSettingsDirectoryName))
                Directory.CreateDirectory(DatabaseImportSettingsDirectoryName);

            var json = JsonSerializer.Serialize(dbImportSettings, options: DefaultJsonSerializerOptions);

            await File.WriteAllTextAsync(GetDatabaseImportSettingsName(dbImportSettings.Name), json, Encoding.Default);
        }

        public async Task SerializeAsync(Database db)
        {
            if (!Directory.Exists(DatabasesDirectoryName))
                Directory.CreateDirectory(DatabasesDirectoryName);

            var json = JsonSerializer.Serialize(db, options: DefaultJsonSerializerOptions);

            await File.WriteAllTextAsync(GetDbFileName(db.Name), json, Encoding.Default);
        }

        public async Task<IEnumerable<DatabaseItemModel>> GetDatabasesAsync()
        {
            if (!Directory.Exists(DatabasesDirectoryName))
                Directory.CreateDirectory(DatabasesDirectoryName);

            var files = Directory.GetFiles(DatabasesDirectoryName);

            var result = new List<DatabaseItemModel>();

            foreach (var item in files)
            {
                var dbInJson = await File.ReadAllTextAsync(item, Encoding.Default);

                var db = JsonSerializer.Deserialize<Database>(dbInJson);

                result.Add(new DatabaseItemModel
                {
                    Name = db.Name,
                    TablesCount = db.Tables.Count,
                    ViewsCount = db.Views.Count
                });
            }

            return result;
        }

        public async Task<DatabaseDetailsModel> GetDatabaseAsync(string name)
        {
            var fileName = GetDbFileName(name);

            if (!File.Exists(fileName))
                return null;

            var dbInJson = await File.ReadAllTextAsync(fileName, Encoding.Default);

            var db = JsonSerializer.Deserialize<Database>(dbInJson);

            return new DatabaseDetailsModel
            {
                Name = db.Name,
                Tables = db.Tables.Select(item => new TableItemModel
                {
                    Schema = item.Schema,
                    Name = item.Name,
                    Type = item.Type,
                    FullName = item.FullName,
                    ColumnsCount = item.Columns.Count,
                    PrimaryKey = item.PrimaryKey == null ? "" : string.Join(",", item.PrimaryKey.Key),
                    Identity = item.Identity == null ? "" : item.Identity.Name
                }).ToList(),
                Views = db.Views.Select(item => new ViewItemModel
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

        public async Task<Table> GetTableAsync(string databaseName, string tableName)
        {
            var fileName = GetDbFileName(databaseName);

            if (!File.Exists(fileName))
                return null;

            var dbInJson = await File.ReadAllTextAsync(fileName, Encoding.Default);

            var db = JsonSerializer.Deserialize<Database>(dbInJson);

            return db.FindTable(tableName);
        }
    }
}
