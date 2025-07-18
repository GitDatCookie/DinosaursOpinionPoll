using AI_Project.DBContext;
using AI_Project.Enums;
using AI_Project.Models.OrderModels;
using AI_Project.Models.QuestionaireComponentModels;
using AI_Project.Models.UserModels.AdminUserComponentModels;
using AI_Project.Models.UserModels.SubjectUserModelComponents;
using AI_Project.Services.Interfaces;
using AI_Project.ViewModels;
using AI_Project.ViewModels.QuestionaireComponentViewModels;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace AI_Project.Services
{
    public class QuestionaireService : IQuestionaireService
    {
        private readonly AI_ProjectDbContext _dbContext;
        private readonly IQuestionaireComponentService _questionaireComponentService;
        public QuestionaireService(AI_ProjectDbContext dbContext, IQuestionaireComponentService questionaireComponentService)
        {
            _dbContext = dbContext;
            _questionaireComponentService = questionaireComponentService;
        }

        public async Task<string> CreateQuestionaireAsync(QuestionaireViewModel questionaireViewModel)
        {
            QuestionaireModel questionaireModel = await CreateModelAsync(questionaireViewModel);
            _dbContext.Add(questionaireModel);
            _dbContext.SaveChanges();
            return questionaireModel.PublicToken;
        }

        public void CreateQuestionairePage(Guid questionaireId, QuestionairePageViewModel pageViewModel)
        {
           //Question questionaireModel = _dbContext.QuestionaireModels.Include(i => i.PageList).Where(x => x.QuestionaireId == questionaireId).FirstOrDefault();

           // QuestionairePageModel page = new QuestionairePageModel()
           // {
           //     OrderID = pageViewModel.OrderID,
           //     RandomGroupID = pageViewModel.RandomGroupID,
           //     IsRandomised = pageViewModel.IsRandomised,
           //     OrderedItems = pageViewModel.Order
           // };

           // questionaireModel.PageList.Add(page);
           // _dbContext.SaveChanges();

        }

        public void DeleteQuestionaire(Guid questionaireId)
        {
            throw new NotImplementedException();
        }

        public void DeleteQuestionairePage(Guid questionairePageId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateQuestionaireAsync(Guid questionaireId, QuestionaireViewModel updatedModel)
        {
            throw new NotImplementedException();
        }

        public void UpdateQuestionairePage(QuestionairePageModel questionairePage)
        {
            throw new NotImplementedException();
        }

        public QuestionaireModel GetQuestionaireById(Guid questionaireId)
        {
            QuestionaireModel questionaire = _dbContext.QuestionaireModels
                .Include(p => p.PageList)
                .Where(x => x.QuestionaireId == questionaireId)
                .FirstOrDefault();
            return questionaire;
        }

        public void GetQuestionairePageById(Guid questionairePageId)
        {
            throw new NotImplementedException();
        }

        public void GetQuestionairePages()
        {
            throw new NotImplementedException();
        }

        public async Task<List<QuestionaireViewModel>> GetQuestionairesAsync()
        {
            List<QuestionaireModel> questionaireModels = [.. _dbContext.QuestionaireModels];
            List<QuestionaireViewModel> viewModels = [];

            foreach(var questionaire in questionaireModels)
            {
                viewModels.Add(await CreateViewModelAsync(questionaire));
            }

            return viewModels;
        }

        public QuestionaireViewModel GetShuffleQuestionairePages(string token)
        {
            QuestionaireViewModel questionaireViewModel = new(); // { QuestionaireId = questionaire.QuestionaireId };
            return questionaireViewModel;
        }
        public async Task <QuestionaireViewModel> GetQuestionaireViewModelByTokenAsync(string token)
        {
            QuestionaireModel questionaireModel = _dbContext.QuestionaireModels
            .Include(questionaire => questionaire.PageList)
                .ThenInclude(page => page.Items)
                    .ThenInclude(item => (item as QuestionModel).Answers)
            .Where(x => x.PublicToken == token)
            .FirstOrDefault();

            return await CreateViewModelAsync(questionaireModel);
        }
        public async Task<QuestionaireViewModel> GetQuestionaireViewModelByTokenAsync(string token, bool isTreatmentGroup)
        {
            QuestionaireModel questionaireModel = new();
                if (isTreatmentGroup == true)
                {
                    questionaireModel = _dbContext.QuestionaireModels
                        .Include(q => q.PageList)
                            .ThenInclude(page => page.Items
                                .Where(item => item.GroupType == EQuestionGroupType.TreatmentGroup ||
                                               item.GroupType == EQuestionGroupType.Both))
                        .Include(q => q.PageList)
                            .ThenInclude(page => page.Items)
                                .ThenInclude(item => ((QuestionModel)item).Answers)
                        .Where(q => q.PublicToken == token)
                        .FirstOrDefault();
                }
                else
                {
                    questionaireModel = _dbContext.QuestionaireModels
                        .Include(q => q.PageList)
                            .ThenInclude(page => page.Items
                                .Where(item => item.GroupType == EQuestionGroupType.ControlGroup ||
                                               item.GroupType == EQuestionGroupType.Both))
                        .Include(q => q.PageList)
                            .ThenInclude(page => page.Items)
                                .ThenInclude(item => ((QuestionModel)item).Answers)
                        .Where(q => q.PublicToken == token)
                        .FirstOrDefault();
                }

            return await CreateViewModelAsync(questionaireModel);

        }

        private string CreatePublikToken()
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[16];
            rng.GetBytes(bytes);

            string token = Convert.ToBase64String(bytes)
                .Replace("/", "_")
                .Replace("+", "-")
                .Replace("=", "");

            if (_dbContext.QuestionaireModels.Where(x => x.PublicToken == token).Any())
            {
                return CreatePublikToken();
            }

            return token;
        }

        private async Task <QuestionaireViewModel> CreateViewModelAsync(QuestionaireModel questionaireModel)
        {
            List<QuestionairePageViewModel> pageViewModels = [];

            foreach (var pageModel in questionaireModel.PageList)
            {
                List<(QuestionaireComponentViewModelBase viewModelBase, EComponentType ItemType, EQuestionComponentType QuestionType)> componentViewModels
                    = [];

                foreach (var componentModel in pageModel.Items)
                {
                    var viewModelBase = await _questionaireComponentService
                        .GetQuestionaireComponentViewModelAsync(componentModel);

                    (EComponentType itemType, EQuestionComponentType questionType) = viewModelBase switch
                    {
                        QuestionViewModel question => (EComponentType.Question, question.QuestionType),
                        VideoViewModel _ => (EComponentType.Video, EQuestionComponentType.None),
                        ImageViewModel _ => (EComponentType.Image, EQuestionComponentType.None),
                        FreeTextViewModel _ => (EComponentType.FreeText, EQuestionComponentType.None),
                        _ => throw new NotSupportedException($"ViewModel type {viewModelBase.GetType().Name} is not supported.")
                    };

                    componentViewModels.Add((viewModelBase, itemType, questionType));
                }

                QuestionairePageViewModel pageViewModel = new()
                {
                    OrderID = pageModel.OrderID,
                    IsRandomised = pageModel.IsRandomised,
                    Items = componentViewModels,
                };

                pageViewModels.Add(pageViewModel);
            }

            QuestionaireViewModel viewModel = new()
            {
                QuestionaireTitle = questionaireModel.QuestionaireTitle,
                PublicToken = questionaireModel.PublicToken,
                PageList = pageViewModels,
            };

            return viewModel;
        }

        private async Task<QuestionaireModel> CreateModelAsync(QuestionaireViewModel questionaireViewModel)
        {
            List<QuestionairePageModel> pageModels = [];
            foreach(var page in questionaireViewModel.PageList)
            {
                List<QuestionaireComponentModelBase> questionaireComponents = [];
                foreach((QuestionaireComponentViewModelBase viewModelBase, EComponentType itemType, EQuestionComponentType questionType) in page.Items)
                {
                    await _questionaireComponentService.UpdateQuestionaireComponentAsync((viewModelBase.Id, viewModelBase, itemType));
                    questionaireComponents.Add(await _questionaireComponentService.GetQuestionaireComponentModelAsync((viewModelBase.Id, itemType)));
                }

                QuestionairePageModel pageModel = new()
                {
                    IsRandomised = true,
                    OrderID = page.OrderID,
                    Items = questionaireComponents,
                };

                pageModels.Add(pageModel);
            }

            QuestionaireModel questionaire = new()
            {
                PageList = pageModels,
                PublicToken = CreatePublikToken(),
                QuestionaireTitle = questionaireViewModel.QuestionaireTitle,
                
            };

            return questionaire;
        }
    }
}
