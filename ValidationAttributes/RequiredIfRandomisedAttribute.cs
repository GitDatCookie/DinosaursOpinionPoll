using System.ComponentModel.DataAnnotations;

namespace AI_Project.ValidationAttributes

{
    public class RequiredIfRandomisedAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var instance = validationContext.ObjectInstance;
            var type = instance.GetType();

            var randomisedProperty = type.GetProperty("Randomised");

            return randomisedProperty != null && randomisedProperty.GetValue(instance) is bool isRandomised && isRandomised && (value == null || (int)value == 0)
                ? new ValidationResult("OrderNumber is required when Randomised is true.")
                : ValidationResult.Success;
        }
    }
}