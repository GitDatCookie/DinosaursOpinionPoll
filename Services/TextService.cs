using AI_Project.DBContext;
using AI_Project.Models.QuestionaireComponentModels;
using AI_Project.Services.Interfaces;
using AI_Project.ViewModels.QuestionaireComponentViewModels;

namespace AI_Project.Services
{
    public class TextService : ITextService
    {
        private readonly AI_ProjectDbContext _dbContext;

        public string userIdentifier = string.Empty;
        public TextService(AI_ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateText(FreeTextViewModel text)
        {
            _dbContext.Add(CreateTextModel(text));
            _dbContext.SaveChanges();
        }
        public void ChangeText(Guid textId, FreeTextViewModel newTextViewModel)
        {
            FreeTextModel textToBeChanged = GetText(textId);
            textToBeChanged.Title = newTextViewModel.Title;
            textToBeChanged.FreeText = newTextViewModel.FreeText;

            _dbContext.SaveChanges();
        }
        public void DeleteText(Guid textId)
        {
            _dbContext.FreeTexts.Remove(GetText(textId));
            _dbContext.SaveChanges();
        }

        public FreeTextViewModel GetTextViewModel(Guid textId)
        {
            FreeTextModel textModel = _dbContext.FreeTexts.Where(x => x.Id == textId).FirstOrDefault();
            return CreateViewModel(textModel);
        }
        public List<FreeTextViewModel> GetTexts()
        {
            List<FreeTextViewModel> result = new();
            foreach(var text in _dbContext.FreeTexts.ToList())
            {
                result.Add(CreateViewModel(text));
            }
            return result;
        }

        private FreeTextViewModel CreateViewModel(FreeTextModel textModel)
        {
            FreeTextViewModel viewModel = new FreeTextViewModel()
            {
                Id = textModel.Id,
                Title = textModel.Title,
                FreeText = textModel.FreeText,
            };

            return viewModel;
        }
        private FreeTextModel CreateTextModel(FreeTextViewModel textViewModel)
        {
            FreeTextModel textModel = new()
            {
                Title = textViewModel.Title,
                FreeText = textViewModel.FreeText
            };
            return textModel;
        }
        private FreeTextModel GetText(Guid textId)
        {
            FreeTextModel textModel = _dbContext.FreeTexts.Where(x => x.Id == textId).FirstOrDefault();
            return textModel;
        }
    }
}
