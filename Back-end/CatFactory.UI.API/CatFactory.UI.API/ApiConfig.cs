namespace CatFactory.UI.API
{
    public class ApiConfig
    {
        public ApiConfig()
        {
            DatabasesDirectoryName = "Databases";
            DatabaseImportSettingsName = "DatabaseImportSettings";
        }

        public string DatabasesDirectoryName { get; set; }

        public string DatabaseImportSettingsName { get; set; }
    }
}
