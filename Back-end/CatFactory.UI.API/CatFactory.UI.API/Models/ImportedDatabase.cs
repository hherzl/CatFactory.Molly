namespace CatFactory.UI.API.Models
{
    public class ImportedDatabase
    {
        public string Name { get; set; }

        public string Dbms { get; set; } = "SQL Server";

        public int TablesCount { get; set; }

        public int ViewsCount { get; set; }

        public string Details { get; } = "Details";
    }
}
