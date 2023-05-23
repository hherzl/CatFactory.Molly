using CatFactory.ObjectRelationalMapping;

namespace CatFactory.Molly.API.Models
{
    public record TableItemModel
    {
        public TableItemModel()
        {
        }

        public TableItemModel(Table table)
        {
            Schema = table.Schema;
            Name = table.Name;
            Type = table.Type;
            FullName = table.FullName;
            ColumnsCount = table.Columns.Count;
            PrimaryKey = table.PrimaryKey == null ? "" : string.Join(",", table.PrimaryKey.Key);
            Identity = table.Identity == null ? "" : $"{table.Identity.Name}({table.Identity.Seed}, {table.Identity.Increment})";
            Description = table.Description;
        }

        public string Schema { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string FullName { get; set; }
        public int ColumnsCount { get; set; }
        public string PrimaryKey { get; set; }
        public string Identity { get; set; }
        public string Description { get; set; }
    }
}
