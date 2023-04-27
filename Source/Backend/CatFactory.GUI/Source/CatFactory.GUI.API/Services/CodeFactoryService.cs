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

        public string GetDatabaseFileName(string name)
            => Path.Combine(DatabasesDirectoryName, name);

        string DatabaseImportSettingsDirectoryName
            => Path.Combine(_webHostingEnvironment.ContentRootPath, _guiSettings.DatabaseImportSettingsName);

        public string GetDatabaseImportSettingsFileName(string name)
            => Path.Combine(DatabaseImportSettingsDirectoryName, name);

        public async Task SerializeAsync(DatabaseImportSettings dbImportSettings)
        {
            if (!Directory.Exists(DatabaseImportSettingsDirectoryName))
                Directory.CreateDirectory(DatabaseImportSettingsDirectoryName);

            var json = JsonSerializer.Serialize(dbImportSettings, options: DefaultJsonSerializerOptions);

            await File.WriteAllTextAsync(GetDatabaseImportSettingsFileName(dbImportSettings.Name), json, Encoding.Default);
        }

        public async Task SerializeAsync(Database db)
        {
            if (!Directory.Exists(DatabasesDirectoryName))
                Directory.CreateDirectory(DatabasesDirectoryName);

            var json = JsonSerializer.Serialize(db, options: DefaultJsonSerializerOptions);

            await File.WriteAllTextAsync(GetDatabaseFileName(db.Name), json, Encoding.Default);
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
                    Dbms = "SQL Server",
                    TablesCount = db.Tables.Count,
                    ViewsCount = db.Views.Count
                });
            }

            return result;
        }

        public async Task<DatabaseImportSettings> GetDatabaseImportSettingsAsync(string name)
        {
            var fileName = GetDatabaseImportSettingsFileName(name);

            if (!File.Exists(fileName))
                return null;

            var dbInJson = await File.ReadAllTextAsync(fileName, Encoding.Default);

            return JsonSerializer.Deserialize<DatabaseImportSettings>(dbInJson);
        }

        public async Task<Database> GetDatabaseAsync(string name)
        {
            var fileName = GetDatabaseFileName(name);

            if (!File.Exists(fileName))
                return null;

            var dbInJson = await File.ReadAllTextAsync(fileName, Encoding.Default);

            return JsonSerializer.Deserialize<Database>(dbInJson);
        }

        public async Task<Table> GetTableAsync(string databaseName, string tableName)
        {
            var db = await GetDatabaseAsync(databaseName);

            return db.FindTable(tableName);
        }

        public async Task<View> GetViewAsync(string databaseName, string viewName)
        {
            var db = await GetDatabaseAsync(databaseName);

            return db.FindView(viewName);
        }

        public async Task<DatabaseDetailsModel> GetDatabaseDetailsAsync(string name)
        {
            var db = (SqlServerDatabase)await GetDatabaseAsync(name);

            return new DatabaseDetailsModel
            {
                Name = db.Name,
                Dbms = db.Dbms,
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
    }
}
