using CatFactory.ObjectRelationalMapping;

namespace CatFactory.Molly.API.Models;

public record ViewDetailsModel
{
    public ViewDetailsModel()
    {
    }

    public ViewDetailsModel(View view)
    {
        FullName = view.FullName;
        Schema = view.Schema;
        Name = view.Name;
        Description = view.Description;

        Identity = new IdentityDetailsModel(view.Identity);
        Columns = view.Columns.Select(item => new ColumnItemModel(item)).ToList();
        Indexes = view.Indexes?.Select(item => new IndexItemModel(item)).ToList();
    }

    public string FullName { get; set; }
    public string Schema { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public IdentityDetailsModel Identity { get; set; }
    public List<ColumnItemModel> Columns { get; set; }
    public List<IndexItemModel> Indexes { get; set; }
}
