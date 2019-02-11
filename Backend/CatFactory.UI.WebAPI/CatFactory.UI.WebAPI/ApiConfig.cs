namespace CatFactory.UI.WebAPI
{
    public class ApiConfig
    {
        public ApiConfig()
        {
            // todo: Load these values from appsettings.json file

            DatabasesDirectoryName = "Databases";
            DatabaseImportSettingsName = "DatabaseImportSettings";
        }

        public string DatabasesDirectoryName { get; set; }

        public string DatabaseImportSettingsName { get; set; }
    }
}
