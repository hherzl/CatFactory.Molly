namespace CatFactory.GUI.API.Models
{
    public class ImportedDatabase
    {
        public ImportedDatabase()
        {
            // todo: add property for DBMS in core package
            Dbms = "SQL Server";
            Details = "Details";
        }

        public string Name { get; set; }
        public string Dbms { get; set; }
        public int TablesCount { get; set; }
        public int ViewsCount { get; set; }
        public string Details { get; }
    }
}
