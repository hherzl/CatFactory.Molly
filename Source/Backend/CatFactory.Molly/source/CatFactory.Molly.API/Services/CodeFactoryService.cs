using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using CatFactory.ObjectRelationalMapping;
using CatFactory.SqlServer;

namespace CatFactory.Molly.API.Services;

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

    public async Task SerializeAsync(SqlServerDatabase db)
    {
        if (!Directory.Exists(DatabasesDirectoryName))
            Directory.CreateDirectory(DatabasesDirectoryName);

        var json = JsonSerializer.Serialize(db, options: DefaultJsonSerializerOptions);

        await File.WriteAllTextAsync(GetDatabaseFileName(db.Name), json, Encoding.Default);
    }

    public async Task<ICollection<Database>> GetDatabasesAsync()
    {
        if (!Directory.Exists(DatabasesDirectoryName))
            Directory.CreateDirectory(DatabasesDirectoryName);

        var files = Directory.GetFiles(DatabasesDirectoryName);

        var result = new Collection<Database>();

        foreach (var item in files)
        {
            var dbInJson = await File.ReadAllTextAsync(item, Encoding.Default);

            var db = JsonSerializer.Deserialize<SqlServerDatabase>(dbInJson);

            result.Add(db);
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

    public async Task<SqlServerDatabase> GetDatabaseAsync(string name)
    {
        var fileName = GetDatabaseFileName(name);

        if (!File.Exists(fileName))
            return null;

        var dbInJson = await File.ReadAllTextAsync(fileName, Encoding.Default);

        return JsonSerializer.Deserialize<SqlServerDatabase>(dbInJson);
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
}
