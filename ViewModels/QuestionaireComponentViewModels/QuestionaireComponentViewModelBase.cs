using AI_Project.Models.OrderModels;
using System.ComponentModel.DataAnnotations;

namespace AI_Project.ViewModels.QuestionaireComponentViewModels
{
    public class QuestionaireComponentViewModelBase
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public OrderModel? Order { get; set; }
        public RandomGroupViewModel RandomGroup{ get; set; }
    }
}
