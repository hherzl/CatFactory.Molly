using CatFactory.ObjectRelationalMapping;

namespace CatFactory.GUI.API.Models
{
    public class DatabaseDetails
    {
        public string Name { get; set; }
        public IEnumerable<TableDetails> Tables { get; set; }
        public IEnumerable<ViewDetails> Views { get; set; }
        public IEnumerable<DatabaseTypeMap> DatabaseTypeMaps { get; set; }
    }
}
