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

            var objectInString = JsonSerializer.Serialize(dbImportSettings, options: DefaultJsonSerializerOptions);

            await File.WriteAllTextAsync(GetDatabaseImportSettingsName(dbImportSettings.Name), objectInString, Encoding.Default);
        }

        public async Task SerializeAsync(Database db)
        {
            if (!Directory.Exists(DatabasesDirectoryName))
                Directory.CreateDirectory(DatabasesDirectoryName);

            var objectInString = JsonSerializer.Serialize(db, options: DefaultJsonSerializerOptions);

            await File.WriteAllTextAsync(GetDbFileName(db.Name), objectInString, Encoding.Default);
        }

        public async Task<IEnumerable<ImportedDatabase>> GetImportedDatabasesAsync()
        {
            if (!Directory.Exists(DatabasesDirectoryName))
                Directory.CreateDirectory(DatabasesDirectoryName);

            var files = Directory.GetFiles(DatabasesDirectoryName);

            var result = new List<ImportedDatabase>();

            foreach (var item in files)
            {
                var objectInString = await File.ReadAllTextAsync(item, Encoding.Default);

                var db = JsonSerializer.Deserialize<Database>(objectInString);

                result.Add(new ImportedDatabase
                {
                    Name = db.Name,
                    TablesCount = db.Tables.Count,
                    ViewsCount = db.Views.Count
                });
            }

            return result;
        }
    }
}
