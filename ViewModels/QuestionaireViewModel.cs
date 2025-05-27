using AI_Project.Models.OrderModels;
using AI_Project.Models.UserModels.AdminUserComponentModels;

namespace AI_Project.ViewModels
{
    public class QuestionaireViewModel
    {
        public Guid QuestionaireId { get; set; }
        public string QuestionaireTitle { get; set; }
        public string PublicToken { get; set; }
        public List<QuestionairePageViewModel>? PageList{ get; set; }
        public List<RandomGroupViewModel>? RandomQuestionaireComponentGroups { get; set; }
        public List<RandomGroupViewModel>? RandomPageGroups { get; set; }

    }
}
