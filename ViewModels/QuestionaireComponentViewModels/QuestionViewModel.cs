using AI_Project.Enums;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace AI_Project.ViewModels.QuestionaireComponentViewModels
{
    public class QuestionViewModel : QuestionaireComponentViewModelBase
    {
        [Required]
        public EQuestionType QuestionType { get; set; }

        #region ForeignTables
        public ComponentStyleViewModel ComponentStyle { get; set; } = new();
        public List<AnswerViewModel> Answers { get; set; } = null!;
        #endregion
    }
}
