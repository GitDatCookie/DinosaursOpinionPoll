using AI_Project.ViewModels.QuestionaireComponentViewModels;

namespace AI_Project.ViewModels
{
    public class AnswerViewModel
    {
        public Guid AnswerId { get; set; }
        public string AnswerText { get; set; }

        public Guid QuestionId { get; set; }
        public QuestionViewModel Question { get; set; }
    }
}
