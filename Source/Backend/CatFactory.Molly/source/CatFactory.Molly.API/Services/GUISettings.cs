namespace CatFactory.Molly.API.Services
{
    public class GUISettings
    {
        public GUISettings()
        {
            DatabasesDirectoryName = "Databases";
            DatabaseImportSettingsName = "DatabaseImportSettings";
        }

        public string DatabasesDirectoryName { get; set; }
        public string DatabaseImportSettingsName { get; set; }
    }
}
