using AI_Project.Models.QuestionaireComponentModels;
using AI_Project.Models.UserModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Project.Models.UserModels.SubjectUserModelComponents
{
    public class AnswerModel
    {
        [Key]
        public Guid AnswerID { get; set; }
        [Required]
        public string Answer { get; set; } = string.Empty;

        #region ForeignTables
        [ForeignKey("Question")]
        public Guid QuestionId { get; set; }
        public QuestionModel Question { get; set; } = null!;

        public ICollection<SubjectUserModel> Users { get; set; } = null!;
        #endregion
    }
}
