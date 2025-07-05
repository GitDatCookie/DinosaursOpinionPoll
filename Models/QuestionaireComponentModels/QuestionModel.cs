using AI_Project.Enums;
using AI_Project.Models.UserModels.SubjectUserModelComponents;
using AI_Project.ValidationAttributes;
using AI_Project.ViewModels;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace AI_Project.Models.QuestionaireComponentModels
{
    public class QuestionModel : QuestionaireComponentModelBase
    {
        [Required]
        public EQuestionType QuestionType { get; set; }

        #region ForeignTables
        public virtual ComponentStyleModel ComponentStyle { get; set; } = new();
        public ICollection<AnswerModel> Answers { get; set; } = null!;
        #endregion
    }
}
