using AI_Project.Enums;
using AI_Project.ViewModels.QuestionaireComponentViewModels;

namespace AI_Project.Services.Interfaces
{
    public interface IQuestionService
    {
        public void CreateQuestion(QuestionViewModel question);
        public void DeleteQuestion(Guid questionId);
        public void ChangeQuestion(Guid questionId, QuestionViewModel newQuestionModel);
        public QuestionViewModel GetQuestionViewModel(Guid questionId);
        public List<QuestionViewModel> GetQuestions();
        public List<QuestionViewModel> GetQuestionsByType(EQuestionType eQuestionType);
    }
}
