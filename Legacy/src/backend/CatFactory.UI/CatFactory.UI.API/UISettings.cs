namespace CatFactory.UI.API
{
#pragma warning disable CS1591
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
#pragma warning restore CS1591
}
