using System.ComponentModel.DataAnnotations;

namespace CatFactory.Molly.API.Models;

public record UpdateDescriptionRequest : IValidatableObject
{
    public string Description { get; set; }

    public bool HasDescription
        => !string.IsNullOrEmpty(Description);

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!HasDescription)
            yield return new ValidationResult("The description is required", new string[] { nameof(Description) });
    }

    public string FixedDescription
        => Description.Replace("'", "\'").Trim();
}
