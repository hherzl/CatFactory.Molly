using CatFactory.ObjectRelationalMapping;

namespace CatFactory.GUI.API.Models
{
    public class DatabaseDetailsModel
    {
        public string Name { get; set; }
        public string Dbms { get; set; }

        public IEnumerable<TableItemModel> Tables { get; set; }
        public IEnumerable<ViewItemModel> Views { get; set; }
        public IEnumerable<DatabaseTypeMap> DatabaseTypeMaps { get; set; }
    }
}
