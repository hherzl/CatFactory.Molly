namespace CatFactory.UI.API.Models
{
    public class ImportDatabaseRequest
    {
        public string Name { get; set; }

        public string ConnectionString { get; set; }

        public bool ImportTables { get; set; }

        public bool ImportViews { get; set; }
    }
}
