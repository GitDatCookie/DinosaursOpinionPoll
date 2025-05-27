using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AI_Project.ValidationAttributes
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? password = value as string;

            if (string.IsNullOrEmpty(password))
            {
                return new ValidationResult("Password is required.");
            }

            // Check minimum length
            if (password.Length < 8)
            {
                return new ValidationResult("Password must be at least 8 characters long.");
            }

            // Check maximum length
            if (password.Length > 42)
            {
                return new ValidationResult("Password must not be longer than 42 characters.");
            }

            // Check for Argentinosaurus
            if (!password.Contains("Argentinosaurus", StringComparison.OrdinalIgnoreCase))
            {
                return new ValidationResult("Password must contain the name 'Argentinosaurus'.");
            }

            // Check for at least 2 special characters
            int specialCharCount = Regex.Matches(password, @"[!@#$%^&*(),.?""{}|<>]").Count;
            if (specialCharCount < 2)
            {
                return new ValidationResult("Password must contain at least 2 special characters.");
            }


            return ValidationResult.Success;
        }
    }
}
