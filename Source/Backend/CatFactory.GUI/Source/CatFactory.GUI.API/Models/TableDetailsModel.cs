﻿namespace CatFactory.GUI.API.Models
{
    public class TableDetailsModel
    {
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
