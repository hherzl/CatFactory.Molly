using CatFactory.ObjectRelationalMapping;

namespace CatFactory.Molly.API.Models
{
    public class ViewItemModel
    {
        public ViewItemModel()
        {
        }

        public ViewItemModel(View view)
        {
            Schema = view.Schema;
            Name = view.Name;
            Type = view.Type;
            FullName = view.FullName;
            ColumnsCount = view.Columns.Count;
            Identity = view.Identity == null ? "" : $"{view.Identity.Name}({view.Identity.Seed}, {view.Identity.Increment})";
            Description = view.Description;
        }
        public string Schema { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string FullName { get; set; }
        public int ColumnsCount { get; set; }
        public string Identity { get; set; }
        public string Description { get; set; }
    }
}
