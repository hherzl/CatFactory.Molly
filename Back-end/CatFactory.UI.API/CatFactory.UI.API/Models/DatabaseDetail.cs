using System.Collections.Generic;
using CatFactory.Mapping;

namespace CatFactory.UI.API.Models
{
    public class DatabaseDetail
    {
        public string Name { get; set; }

        public IEnumerable<TableDetail> Tables { get; set; }

        public IEnumerable<ViewDetail> Views { get; set; }

        public IEnumerable<DatabaseTypeMap> Mappings { get; set; }
    }
}
