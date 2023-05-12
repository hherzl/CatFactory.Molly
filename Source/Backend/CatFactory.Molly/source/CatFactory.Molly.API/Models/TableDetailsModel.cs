using CatFactory.ObjectRelationalMapping;

namespace CatFactory.Molly.API.Models
{
    public class TableDetailsModel
    {
        public TableDetailsModel()
        {
        }

        public TableDetailsModel(Table table)
        {
            FullName = table.FullName;
            Schema = table.Schema;
            Name = table.Name;
            Description = table.Description;
            Identity = new IdentityDetailsModel(table.Identity);

            Columns = table.Columns.Select(item => new ColumnItemModel(item)).ToList();
            PrimaryKey = new PrimaryKeyDetailsModel(table.PrimaryKey);
            ForeignKeys = table.ForeignKeys?.Select(item => new ForeignKeyItemModel(item)).ToList();
            Uniques = table.Uniques?.Select(item => new UniqueItemModel(item)).ToList();
            Checks = table.Checks?.Select(item => new CheckItemModel(item)).ToList();
            Defaults = table.Defaults?.Select(item => new DefaultItemModel(item)).ToList();
            Indexes = table.Indexes?.Select(item => new IndexItemModel(item)).ToList();
        }

        public string FullName { get; set; }
        public string Schema { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IdentityDetailsModel Identity { get; set; }
        public List<ColumnItemModel> Columns { get; set; }
        public PrimaryKeyDetailsModel PrimaryKey { get; set; }
        public List<ForeignKeyItemModel> ForeignKeys { get; set; }
        public List<UniqueItemModel> Uniques { get; set; }
        public List<CheckItemModel> Checks { get; set; }
        public List<DefaultItemModel> Defaults { get; set; }
        public List<IndexItemModel> Indexes { get; set; }
    }
}
