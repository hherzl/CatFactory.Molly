using CatFactory.ObjectRelationalMapping;

namespace CatFactory.Molly.API.Models
{
    public record DefaultItemModel
    {
        public DefaultItemModel()
        {
        }

        public DefaultItemModel(Default constraint)
        {
            Name = constraint.ConstraintName;
            Key = constraint.Key;
            Value = constraint.Value;
        }

        public string Name { get; set; }
        public List<string> Key { get; set; }
        public string Value { get; set; }
    }
}
