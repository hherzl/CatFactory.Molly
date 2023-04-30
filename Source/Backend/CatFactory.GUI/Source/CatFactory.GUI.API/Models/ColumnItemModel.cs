using CatFactory.ObjectRelationalMapping;

namespace CatFactory.GUI.API.Models
{
    public class ColumnItemModel
    {
        public ColumnItemModel()
        {
        }

        public ColumnItemModel(Column column)
        {
            Name = column.Name;
            Type = column.Type;
            Length = column.Length;
            Prec = column.Prec;
            Nullable = column.Nullable;
            Collation = column.Collation;
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public int Length { get; set; }
        public short Prec { get; set; }
        public bool Nullable { get; set; }
        public string Collation { get; set; }
    }
}
