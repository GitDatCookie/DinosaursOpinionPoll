using AI_Project.Models.OrderModels;
using AI_Project.Models.QuestionaireComponentModels;
using System.ComponentModel.DataAnnotations;

namespace AI_Project.Models.UserModels.AdminUserComponentModels
{
    public class QuestionairePageModel
    {
        [Key]
        public Guid PageID { get; set; }
        public int OrderID { get; set; }
        public bool IsRandomised { get; set; }

        public RandomGroupModel? RandomGroup { get; set; }
        public virtual ICollection<QuestionaireComponentModelBase> Items { get; set; }
    }
}
