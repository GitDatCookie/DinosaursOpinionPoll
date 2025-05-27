using AI_Project.Enums;
using AI_Project.ViewModels.QuestionaireComponentViewModels;

namespace AI_Project.Services.Interfaces
{
    public interface IComponentFactory
    {
        QuestionaireComponentViewModelBase CreateComponent(QuestionaireComponentViewModelBase viewModel);
    }
}
