using AI_Project.DBContext;
using AI_Project.Enums;
using AI_Project.Models.QuestionaireComponentModels;
using AI_Project.Models.UserModels.SubjectUserModelComponents;
using AI_Project.Services.Interfaces;
using AI_Project.ViewModels.QuestionaireComponentViewModels;
using Microsoft.EntityFrameworkCore;

namespace AI_Project.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly AI_ProjectDbContext _dbContext;

        public string userIdentifier = string.Empty;
        public QuestionService(AI_ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateQuestion(QuestionViewModel question)
        {
            _dbContext.Add(CreateModel(question));
            _dbContext.SaveChanges();
        }

        public void ChangeQuestion(Guid questionId, QuestionViewModel newQuestionModel)
        {
            QuestionModel questionToBeChanged = GetQuestion(questionId);
            questionToBeChanged.Title = newQuestionModel.Title != questionToBeChanged.Title 
                ? newQuestionModel.Title 
                : questionToBeChanged.Title;

            questionToBeChanged.ComponentColour = newQuestionModel.ComponentColour;
            // Convert collection of answers to a list for indexing.
            List<AnswerModel> dbAnswers = questionToBeChanged.Answers.ToList();

            // Determine the number of answers to compare.
            int minCount = Math.Min(dbAnswers.Count, newQuestionModel.Answers.Count);

            // Loop through each answer up to the minimum count.
            for (int i = 0; i < minCount; i++)
            {
                if (dbAnswers[i].Answer != newQuestionModel.Answers[i])
                {
                    dbAnswers[i].Answer = newQuestionModel.Answers[i];
                }
            }

            // If the viewmodel contains more answers than the database model, add them.
            if (newQuestionModel.Answers.Count > dbAnswers.Count)
            {
                for (int i = dbAnswers.Count; i < newQuestionModel.Answers.Count; i++)
                {
                    dbAnswers.Add(new AnswerModel
                    {
                        // Generate a new answer.
                        Answer = newQuestionModel.Answers[i]
                    });
                }
                questionToBeChanged.Answers = dbAnswers;
            }
            _dbContext.SaveChanges();
        }

        public void DeleteQuestion(Guid questionId)
        {
            _dbContext.Questions.Remove(GetQuestion(questionId));
            _dbContext.SaveChanges();
        }

        public QuestionViewModel GetQuestionViewModel(Guid questionId)
        {
            QuestionModel questionModel = _dbContext.Questions.Where(x => x.Id == questionId).FirstOrDefault();
            return CreateViewModel(questionModel);
        }

        public List<QuestionViewModel> GetQuestions()
        {
            List<QuestionViewModel> result = new List<QuestionViewModel>();
            foreach (var question in _dbContext.Questions.Include(x => x.Answers))
            {
                result.Add(CreateViewModel(question));
            }
            return result;
        }

        public List<QuestionViewModel> GetQuestionsByType(EQuestionType eQuestionType)
        {
            List<QuestionViewModel> result = new List<QuestionViewModel>();
            foreach (var question in _dbContext.Questions.Include(x => x.Answers).Where(x => x.QuestionType == eQuestionType))
            {
                result.Add(CreateViewModel(question));
            }
            return result;
        }

        private QuestionViewModel CreateViewModel(QuestionModel questionModel)
        {
            List<string> answers = new();
            foreach (var answer in questionModel.Answers)
            {
                answers.Add(answer.Answer);
            }
            QuestionViewModel questionViewModel = new QuestionViewModel()
            {
                Id = questionModel.Id,
                Title = questionModel.Title,
                QuestionType = questionModel.QuestionType,
                ComponentColour = questionModel.ComponentColour,
                Answers = answers
            };

            return questionViewModel;
        }

        private QuestionModel CreateModel(QuestionViewModel questionViewModel)
        {
            List<AnswerModel> answerModelList = new List<AnswerModel>();
            foreach (var answer in questionViewModel.Answers)
            {
                AnswerModel answerModel = new AnswerModel()
                {
                    Answer = answer,
                };
                answerModelList.Add(answerModel);
            }
            QuestionModel questionModel = new QuestionModel()
            {
                Title = questionViewModel.Title,
                QuestionType = questionViewModel.QuestionType,
                ComponentColour = questionViewModel.ComponentColour,
                Answers = answerModelList
            };
            return questionModel;
        }

        private QuestionModel GetQuestion(Guid questionId)
        {
            QuestionModel questionModel = _dbContext.Questions.Include(x => x.Answers).Where(x => x.Id == questionId).FirstOrDefault();
            return questionModel;
        }
    }
}
