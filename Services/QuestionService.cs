using AI_Project.DBContext;
using AI_Project.Enums;
using AI_Project.Models;
using AI_Project.Models.OrderModels;
using AI_Project.Models.QuestionaireComponentModels;
using AI_Project.Models.QuestionaireComponentModels.StyleComponents;
using AI_Project.Models.UserModels.SubjectUserModelComponents;
using AI_Project.Services.Interfaces;
using AI_Project.ViewModels;
using AI_Project.ViewModels.QuestionaireComponentViewModels;
using AI_Project.ViewModels.QuestionaireComponentViewModels.StyleComponentViewModels;
using Microsoft.EntityFrameworkCore;

namespace AI_Project.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly AI_ProjectDbContext _dbContext;
        public QuestionService(AI_ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #region CRUD
        public async Task CreateQuestionAsync(QuestionViewModel viewModel)
        {
            _dbContext.Questions.Add(ToModel(viewModel));
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateQuestionAsync(Guid questionId, QuestionViewModel updatedModel)
        {
            QuestionModel model = await GetQuestionAsync(questionId) ?? throw new KeyNotFoundException($"Question {questionId} not found.");
            model.Title = updatedModel.Title;
            model.TitleFieldType = updatedModel.TitleFieldType;
            model.QuestionType = updatedModel.QuestionType;
            model.IsRandomised = updatedModel.IsRandomised;

            ComponentStyleViewToModel(updatedModel.ComponentStyle, model.ComponentStyle);

            SyncAnswers(model, updatedModel.Answers);

            if (updatedModel.RandomGroup != null)
            {
                RandomGroupModel? randomGroupModel = await _dbContext.RandomGroups
                    .FirstOrDefaultAsync(randomGroup => randomGroup.Id == updatedModel.RandomGroup.Id);

                model.RandomGroup = randomGroupModel
                    ?? new RandomGroupModel
                    {
                        GroupName = updatedModel.RandomGroup.GroupName,
                        RandomGroupType = updatedModel.RandomGroup.RandomGroupType,
                    };
            }

            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteQuestionAsync(Guid questionId)
        {
            var entity = await _dbContext.Questions.FindAsync(questionId);
            if (entity != null)
            {
                _dbContext.Questions.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
        #endregion

        #region GetQuestions
        public async Task<QuestionModel> GetQuestionAsync(Guid questionId)
        {
            var entity = await _dbContext.Questions
                .Include(x => x.ComponentStyle)
                .Include(x => x.Answers)
                .FirstOrDefaultAsync(x => x.Id == questionId);

            return entity ?? throw new KeyNotFoundException($"Question {questionId} not found.");
        }
        public async Task<QuestionViewModel> GetQuestionViewModelAsync(Guid questionId)
        {
            return ToViewModel(await GetQuestionAsync(questionId));
        }
        public async Task<List<QuestionViewModel>> GetQuestionViewModelsAsync()
        {
            var list = await _dbContext.Questions
                .Include(q => q.ComponentStyle)
                .Include(q => q.Answers)
                .ToListAsync();

            return [.. list.Select(ToViewModel)];
        }
        public async Task<List<QuestionViewModel>> GetQuestionsByTypeAsync(EQuestionComponentType questionType)
        {
            var list = await _dbContext.Questions
                .AsNoTracking()
                .Where(q => q.QuestionType == questionType)
                .Include(q => q.ComponentStyle)
                .Include(q => q.Answers)
                .ToListAsync();

            return [.. list.Select(ToViewModel)];
        }
        #endregion

        #region Mapping Helpers

        //RANDOM GROUP NICHT VERGESSEN
        private QuestionModel ToModel(QuestionViewModel viewModel)
        {
            var answerModels = viewModel.Answers != null
            ? [.. viewModel.Answers
                .Where(answers => answers != null)
                .Select(answer => new AnswerModel { Answer = answer.AnswerText })]
            : new List<AnswerModel>();

            var componentStyleModel = new ComponentStyleModel();
            ComponentStyleViewToModel(viewModel.ComponentStyle, componentStyleModel);

            return new QuestionModel
            {
                Title = viewModel.Title,
                QuestionType = viewModel.QuestionType,
                TitleFieldType = viewModel.TitleFieldType,
                IsRandomised = viewModel.IsRandomised,
                ComponentStyle = componentStyleModel,
                Answers = answerModels,
            };
        }

        //RANDOM GROUP NICHT VERGESSEN
        private QuestionViewModel ToViewModel(QuestionModel model)
        {
            var answers = model.Answers != null
                ? model.Answers
                    .Select(a => new AnswerViewModel
                    {
                        AnswerId = a.AnswerID,
                        AnswerText = a.Answer
                    })
                    .ToList()
                : [];

            var componentStyleViewModel = new ComponentStyleViewModel();
            ComponentStyleModelToView(model.ComponentStyle, componentStyleViewModel);

            var viewModel = new QuestionViewModel
            {
                Id = model.Id,
                Title = model.Title,
                QuestionType = model.QuestionType,
                TitleFieldType = model.TitleFieldType,
                IsRandomised = model.IsRandomised,
                Answers = answers,
                ComponentStyle = componentStyleViewModel
            };

            return viewModel;
        }

        private void ComponentStyleViewToModel(ComponentStyleViewModel viewModel, ComponentStyleModel model)
        {
            model.ComponentColour = viewModel.ComponentColour;
            model.IsLabelColourised = viewModel.IsLabelColourised;
            model.Label = viewModel.Label;
            model.Placeholder = viewModel.Placeholder;
            model.TextVariant = viewModel.TextVariant;
            model.QuestionAnswerFieldType = viewModel.QuestionAnswerFieldType;
            model.HelperText = viewModel.HelperText;

            if (viewModel.NumericFieldStyle != null)
            {
                model.NumberFieldStyle = new NumberFieldStyleModel()
                {
                    NumberType = viewModel.NumericFieldStyle.NumberType,
                    MaxNumberFloat = viewModel.NumericFieldStyle.MaxNumberFloat,
                    MinNumberFloat = viewModel.NumericFieldStyle.MinNumberFloat,
                    MaxNumberInteger = viewModel.NumericFieldStyle.MaxNumberInteger,
                    MinNumberInteger = viewModel.NumericFieldStyle.MinNumberInteger,
                };
            }
        }
        private void ComponentStyleModelToView(ComponentStyleModel model, ComponentStyleViewModel viewModel)
        {
            viewModel.ComponentColour = model.ComponentColour;
            viewModel.IsLabelColourised = model.IsLabelColourised;
            viewModel.Label = model.Label;
            viewModel.Placeholder = model.Placeholder;
            viewModel.TextVariant = model.TextVariant;
            viewModel.QuestionAnswerFieldType = model.QuestionAnswerFieldType;
            viewModel.HelperText = model.HelperText;

            if (model.NumberFieldStyle != null)
            {
                viewModel.NumericFieldStyle = new NumericFieldStyleViewModel
                {
                    NumberType = model.NumberFieldStyle.NumberType,
                    MaxNumberFloat = model.NumberFieldStyle.MaxNumberFloat,
                    MinNumberFloat = model.NumberFieldStyle.MinNumberFloat,
                    MaxNumberInteger = model.NumberFieldStyle.MaxNumberInteger,
                    MinNumberInteger = model.NumberFieldStyle.MinNumberInteger
                };
            }
        }
        private void SyncAnswers(QuestionModel model, IList<AnswerViewModel> updatedAnswers)
        {
            List<AnswerModel> removedAnswers = [.. model.Answers
                .Where(modelAnswers => updatedAnswers
                .All(viewModelAnswers => viewModelAnswers.AnswerId != modelAnswers.AnswerID))];
            _dbContext.Answers.RemoveRange(removedAnswers);

            foreach (var answer in updatedAnswers)
            {
                var existing = model.Answers
                    .FirstOrDefault(modelAnswer => modelAnswer.AnswerID == answer.AnswerId);

                if (existing != null)
                    existing.Answer = answer.AnswerText;
                else
                    model.Answers.Add(new AnswerModel { Answer = answer.AnswerText });
            }
        }
        #endregion
    }
}
