using AI_Project.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace AI_Project.ViewModels
{
    public class AdminUserViewModel
    {
        [Required]
        public string UserName { get; set; } = null!;

        [PasswordValidation]
        [Required]
        public string Password { get; set; } = null!;
    }
}
