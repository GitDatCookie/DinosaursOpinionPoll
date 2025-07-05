using AI_Project.Enums;
using AI_Project.Models.QuestionaireComponentModels;
using AI_Project.Models.UserModels.AdminUserComponentModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Project.Models.OrderModels
{
    public class RandomGroupModel
    {
        [Key]
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public ERandomGroupType RandomGroupType { get; set; }

        public virtual ICollection<QuestionaireComponentModelBase> QuestionaireComponents { get; set; }
        public virtual ICollection<QuestionairePageModel> QuestionairePages { get; set; }
    }
}
