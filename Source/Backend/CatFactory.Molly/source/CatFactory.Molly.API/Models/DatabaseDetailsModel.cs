using CatFactory.ObjectRelationalMapping;

namespace CatFactory.Molly.API.Models;

public record DatabaseDetailsModel
{
    public DatabaseDetailsModel()
    {
    }

    public DatabaseDetailsModel(Database database)
    {
        Name = database.Name;
        Dbms = database.Dbms;
        Description = database.Description;
        Tables = database.Tables.Select(item => new TableItemModel(item)).ToList();
        Views = database.Views.Select(item => new ViewItemModel(item)).ToList();
        DatabaseTypeMaps = database.DatabaseTypeMaps;
    }

    public string Name { get; set; }
    public string Dbms { get; set; }
    public string Description { get; set; }

    public IEnumerable<TableItemModel> Tables { get; set; }
    public IEnumerable<ViewItemModel> Views { get; set; }
    public IEnumerable<DatabaseTypeMap> DatabaseTypeMaps { get; set; }
}
