using CatFactory.ObjectRelationalMapping;

namespace CatFactory.Molly.API.Models
{
    public class CheckItemModel
    {
        public CheckItemModel()
        {
        }

        public CheckItemModel(Check constraint)
        {
            Name = constraint.ConstraintName;
            Key = constraint.Key;
            Expression = constraint.Expression;
        }

        public string Name { get; set; }
        public List<string> Key { get; set; }
        public string Expression { get; set; }
    }
}
