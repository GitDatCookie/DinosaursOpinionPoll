using AI_Project.ViewModels;

namespace AI_Project.Services.Interfaces
{
    public interface IAnswerService
    {
        public Task CreateAnswer(Guid questionId, AnswerViewModel answer);
        public Task DeleteAnswer(Guid answerId);
        public Task ChangeAnswer(AnswerViewModel answer);
        public AnswerViewModel GetAnswer(Guid answerId);
        public List<AnswerViewModel> GetAnswers();
    }
}
