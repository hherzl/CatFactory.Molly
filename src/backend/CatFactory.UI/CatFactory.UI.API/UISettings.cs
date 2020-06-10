namespace CatFactory.UI.API
{
    public class UISettings
    {
        public UISettings()
        {
            // todo: Load these values from appsettings.json file

            DatabasesDirectoryName = "Databases";
            DatabaseImportSettingsName = "DatabaseImportSettings";
        }

        public string DatabasesDirectoryName { get; set; }

        public string DatabaseImportSettingsName { get; set; }
    }
}
