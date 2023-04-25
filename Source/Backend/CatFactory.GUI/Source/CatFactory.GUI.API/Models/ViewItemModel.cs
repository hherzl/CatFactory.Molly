namespace CatFactory.GUI.API.Models
{
    public class ViewItemModel
    {
        public string Schema { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string FullName { get; set; }
        public int ColumnsCount { get; set; }
        public string Identity { get; set; }
        public string Details { get; } = "Details";
    }
}
