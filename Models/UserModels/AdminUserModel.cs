using AI_Project.Models.QuestionaireComponentModels;
using AI_Project.Models.UserModels.AdminUserComponentModels;
using System.ComponentModel.DataAnnotations;

namespace AI_Project.Models.UserModels
{
    public class AdminUserModel
    {
        [Key]
        public Guid LoginId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;

        public ICollection<QuestionModel> Questions { get; set; }
        public ICollection<QuestionaireModel> Questionaires { get; set; }
    }
}
