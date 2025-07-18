using AI_Project.Enums;
using AI_Project.Models.OrderModels;
using System.ComponentModel.DataAnnotations;

namespace AI_Project.Models.QuestionaireComponentModels
{
    public class QuestionaireComponentModelBase
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public EComponentTitleFieldType TitleFieldType { get; set; }
        public bool IsRandomised { get; set; }
        public EQuestionGroupType GroupType { get; set; } = EQuestionGroupType.Both;
        public RandomGroupModel? RandomGroup { get; set; }
    }
}
