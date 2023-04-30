using CatFactory.ObjectRelationalMapping;

namespace CatFactory.GUI.API.Models
{
    public class DefaultItemModel
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
