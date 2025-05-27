using AI_Project.Models.OrderModels;
using System.ComponentModel.DataAnnotations;

namespace AI_Project.Models.QuestionaireComponentModels
{
    public class QuestionaireComponentModelBase
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }

        public RandomGroupModel? RandomGroup { get; set; }
    }
}
