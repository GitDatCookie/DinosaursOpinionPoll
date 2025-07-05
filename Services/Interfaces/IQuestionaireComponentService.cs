using AI_Project.Enums;
using AI_Project.Models.QuestionaireComponentModels;
using AI_Project.ViewModels.QuestionaireComponentViewModels;

namespace AI_Project.Services.Interfaces
{
    public interface IQuestionaireComponentService
    {
        Task CreateQuestionaireComponentAsync((QuestionaireComponentViewModelBase viewModel, EItemType itemType) questionaireComponent);
        Task DeleteQuestionaireComponentAsync((Guid questionaireComponentId, EItemType itemType) questionaireComponent);
        Task UpdateQuestionaireComponentAsync((Guid questionaireComponentId, QuestionaireComponentViewModelBase viewModel, EItemType itemType) questionaireComponent);
        Task<QuestionaireComponentModelBase> GetQuestionaireComponentModelAsync((Guid questionaireComponentId, EItemType itemType) questionaireComponent);
        Task<QuestionaireComponentViewModelBase> GetQuestionaireComponentViewModelAsync(QuestionaireComponentModelBase questionaireComponent);

    }
}
