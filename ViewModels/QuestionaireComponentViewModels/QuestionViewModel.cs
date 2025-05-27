using AI_Project.Enums;
using System.ComponentModel.DataAnnotations;

namespace AI_Project.ViewModels.QuestionaireComponentViewModels
{
    public class QuestionViewModel : QuestionaireComponentViewModelBase
    {
        [Required]
        public EQuestionType QuestionType { get; set; }

        public string ComponentColour { get; set; }

        #region ForeignTables
        public List<string> Answers { get; set; } = null!;
        #endregion
    }
}
