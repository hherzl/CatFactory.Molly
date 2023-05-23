using CatFactory.ObjectRelationalMapping;

namespace CatFactory.Molly.API.Models
{
    public record PrimaryKeyDetailsModel
    {
        public PrimaryKeyDetailsModel()
        {
        }

        public PrimaryKeyDetailsModel(PrimaryKey constraint)
        {
            if (constraint == null)
                return;

            ConstraintName = constraint.ConstraintName;
            Key = constraint.Key;
        }

        public string ConstraintName { get; set; }
        public List<string> Key { get; set; }
    }
}
