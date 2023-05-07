namespace CatFactory.GUI.API.Models
{
    public class ViewDetailsModel
    {
        public string FullName { get; set; }
        public string Schema { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IdentityDetailsModel Identity { get; set; }
        public List<ColumnItemModel> Columns { get; set; }
        public List<IndexItemModel> Indexes { get; set; }
    }
}
