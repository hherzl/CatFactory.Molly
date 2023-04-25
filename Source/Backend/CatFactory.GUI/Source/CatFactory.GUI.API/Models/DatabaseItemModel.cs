namespace CatFactory.GUI.API.Models
{
    public class DatabaseItemModel
    {
        public DatabaseItemModel()
        {
            // todo: add property for DBMS in core package
            Dbms = "SQL Server";
        }

        public string Name { get; set; }
        public string Dbms { get; set; }
        public int TablesCount { get; set; }
        public int ViewsCount { get; set; }
    }
}
