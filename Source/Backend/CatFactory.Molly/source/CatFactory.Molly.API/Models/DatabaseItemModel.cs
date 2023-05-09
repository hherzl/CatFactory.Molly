namespace CatFactory.Molly.API.Models
{
    public class DatabaseItemModel
    {
        public DatabaseItemModel()
        {
        }

        public DatabaseItemModel(string name, string dbms, int tablesCount, int viewsCount)
        {
            Name = name;
            Dbms = dbms;
            TablesCount = tablesCount;
            ViewsCount = viewsCount;
        }

        public string Name { get; set; }
        public string Dbms { get; set; }
        public int TablesCount { get; set; }
        public int ViewsCount { get; set; }
    }
}
