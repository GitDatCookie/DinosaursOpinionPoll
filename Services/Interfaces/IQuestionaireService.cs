using AI_Project.Models.UserModels.AdminUserComponentModels;
using AI_Project.ViewModels;

namespace AI_Project.Services.Interfaces
{
    public interface IQuestionaireService
    {
        public Task<string> CreateQuestionaireAsync(QuestionaireViewModel questionaire);
        public void CreateQuestionairePage(Guid questionaireId, QuestionairePageViewModel pageViewModel);
        public void DeleteQuestionaire(Guid questionaireId);
        public void DeleteQuestionairePage(Guid questionairePageId);
        public void EditQuestionaire(QuestionaireModel questionaire);
        public void EditQuestionairePage(QuestionairePageModel questionairePage);

        public QuestionaireModel GetQuestionaireById(Guid questionaireId);

        public Task<List<QuestionaireViewModel>> GetQuestionairesAsync();

        public void GetQuestionairePageById(Guid questionairePageId);
        public void GetQuestionairePages();
        public Task<QuestionaireViewModel> GetQuestionaireViewModelByTokenAsync(string token);
        public Task <QuestionaireViewModel> GetQuestionaireViewModelByTokenAsync(string token, bool isTreatmentGroup);





    }
}
