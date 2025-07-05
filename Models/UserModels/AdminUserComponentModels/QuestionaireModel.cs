using AI_Project.Models.OrderModels;
using AI_Project.Models.QuestionaireComponentModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Project.Models.UserModels.AdminUserComponentModels
{
    public class QuestionaireModel
    {
        [Key]
        public Guid QuestionaireId { get; set; }
        public string QuestionaireTitle { get; set; }
        public string PublicToken { get; set; }

        public virtual ICollection<QuestionairePageModel> PageList { get; set; }

    }
}
