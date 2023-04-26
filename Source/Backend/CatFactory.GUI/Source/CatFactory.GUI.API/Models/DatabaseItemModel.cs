namespace CatFactory.GUI.API.Models
{
    public class DatabaseItemModel
    {
        public string Name { get; set; }
        public string Dbms { get; set; }
        public int TablesCount { get; set; }
        public int ViewsCount { get; set; }
    }
}
