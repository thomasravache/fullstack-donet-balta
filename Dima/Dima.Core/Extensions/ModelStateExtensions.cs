using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Extensions;

public static class ModelStateExtensions
{
    public static List<string> GetErrorsFromValidationContext(this object model)
    {
        List<ValidationResult> results = [];
        var context = new ValidationContext(model);

        Validator.TryValidateObject(model, context, results, true);

        return results
            .Where(x => x.ErrorMessage != null)
            .Select(x => x?.ErrorMessage ?? string.Empty)
            .ToList();
    }
}