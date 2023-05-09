using CatFactory.ObjectRelationalMapping;

namespace CatFactory.Molly.API.Models
{
    public class IdentityDetailsModel
    {
        public IdentityDetailsModel()
        {
        }

        public IdentityDetailsModel(Identity identity)
        {
            if (identity == null)
                return;

            Name = identity.Name;
            Seed = identity.Seed;
            Increment = identity.Increment;
        }

        public string Name { get; set; }
        public int Seed { get; set; }
        public int Increment { get; set; }
    }
}
