using AI_Project.Models.QuestionaireComponentModels;
using AI_Project.ViewModels.QuestionaireComponentViewModels;

namespace AI_Project.Services.Interfaces
{
    public interface ITextService
    {
        public void CreateText(FreeTextViewModel text);
        public void DeleteText(Guid textId);
        public void ChangeText(Guid textId, FreeTextViewModel newTextViewModel);
        public FreeTextModel GetText(Guid textId);
        public FreeTextViewModel GetTextViewModel(Guid textId);
        public List<FreeTextViewModel> GetTexts();
    }
}
