using AI_Project.Models.UserModels.AdminUserComponentModels;
using AI_Project.ViewModels;

namespace AI_Project.Services.Interfaces
{
    public interface IQuestionaireService
    {
        public void CreateQuestionaire(QuestionaireViewModel questionaire);
        public void CreateQuestionairePage(Guid questionaireId, QuestionairePageViewModel pageViewModel);
        public void DeleteQuestionaire(Guid questionaireId);
        public void DeleteQuestionairePage(Guid questionairePageId);
        public void EditQuestionaire(QuestionaireModel questionaire);
        public void EditQuestionairePage(QuestionairePageModel questionairePage);

        public QuestionaireModel GetQuestionaireById(Guid questionaireId);

        public void GetQuestionaires();

        public void GetQuestionairePageById(Guid questionairePageId);
        public void GetQuestionairePages();





    }
}
