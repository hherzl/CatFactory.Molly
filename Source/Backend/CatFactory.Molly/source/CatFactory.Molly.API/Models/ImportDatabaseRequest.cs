using System.ComponentModel.DataAnnotations;

namespace CatFactory.Molly.API.Models
{
    public record ImportDatabaseRequest : IValidatableObject
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public bool ImportTables { get; set; }
        public bool ImportViews { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Name))
                yield return new ValidationResult("The name is required", new string[] { nameof(Name) });

            if (string.IsNullOrEmpty(ConnectionString))
                yield return new ValidationResult("The connection string is required", new string[] { nameof(ConnectionString) });

            if (!ImportTables && !ImportViews)
                yield return new ValidationResult("There is no setting to import", new string[] { nameof(ImportTables), nameof(ImportViews) });
        }
    }
}
