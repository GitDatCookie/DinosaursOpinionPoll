using AI_Project.Enums;
using AI_Project.Models.UserModels.SubjectUserModelComponents;
using AI_Project.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace AI_Project.Models.QuestionaireComponentModels
{
    public class QuestionModel : QuestionaireComponentModelBase
    {
        [Required]
        public EQuestionType QuestionType { get; set; }

        public string ComponentColour { get; set; }
        #region ForeignTables
        public ICollection<AnswerModel> Answers { get; set; } = null!;
        #endregion
    }
}
