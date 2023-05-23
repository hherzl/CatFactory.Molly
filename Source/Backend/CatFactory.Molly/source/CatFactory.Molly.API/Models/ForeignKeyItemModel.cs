using CatFactory.ObjectRelationalMapping;

namespace CatFactory.Molly.API.Models
{
    public record ForeignKeyItemModel
    {
        public ForeignKeyItemModel()
        {
        }

        public ForeignKeyItemModel(ForeignKey constraint)
        {
            Name = constraint.ConstraintName;
            Key = constraint.Key;
            References = constraint.References;
        }

        public string Name { get; set; }
        public List<string> Key { get; set; }
        public string References { get; set; }
    }
}
