using System.ComponentModel.DataAnnotations;

namespace AI_Project.Models
{
    public class EmailModel
    {
        [Key]
        public string Email { get; set; } = null!;
    }
}
