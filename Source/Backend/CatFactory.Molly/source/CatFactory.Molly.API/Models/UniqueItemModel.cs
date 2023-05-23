using CatFactory.ObjectRelationalMapping;

namespace CatFactory.Molly.API.Models
{
    public record UniqueItemModel
    {
        public UniqueItemModel()
        {
        }

        public UniqueItemModel(Unique constraint)
        {
            Name = constraint.ConstraintName;
            Key = constraint.Key;
        }

        public string Name { get; set; }
        public List<string> Key { get; set; }
    }
}
