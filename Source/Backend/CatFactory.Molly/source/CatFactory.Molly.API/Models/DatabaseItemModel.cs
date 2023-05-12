using CatFactory.ObjectRelationalMapping;

namespace CatFactory.Molly.API.Models
{
    public class DatabaseItemModel
    {
        public DatabaseItemModel()
        {
        }

        public DatabaseItemModel(Database database)
        {
            Name = database.Name;
            Dbms = database.Dbms;
            TablesCount = database.Tables.Count;
            ViewsCount = database.Views.Count;
        }

        public string Name { get; set; }
        public string Dbms { get; set; }
        public int TablesCount { get; set; }
        public int ViewsCount { get; set; }
    }
}
