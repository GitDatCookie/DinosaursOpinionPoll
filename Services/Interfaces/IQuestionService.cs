using AI_Project.Enums;
using AI_Project.Models.QuestionaireComponentModels;
using AI_Project.ViewModels.QuestionaireComponentViewModels;

namespace AI_Project.Services.Interfaces
{
    public interface IQuestionService
    {
        Task CreateQuestionAsync(QuestionViewModel question);
        Task UpdateQuestionAsync(Guid questionId, QuestionViewModel newQuestionModel);
        Task DeleteQuestionAsync(Guid questionId);

        Task<QuestionModel> GetQuestionAsync(Guid questionId);
        Task<QuestionViewModel> GetQuestionViewModelAsync(Guid questionId);
        Task<List<QuestionViewModel>> GetQuestionViewModelsAsync();
        Task<List<QuestionViewModel>> GetQuestionsByTypeAsync(EQuestionType eQuestionType);
    }
}
