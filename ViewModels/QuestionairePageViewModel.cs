using AI_Project.Enums;
using AI_Project.Models.OrderModels;
using AI_Project.ViewModels.QuestionaireComponentViewModels;

namespace AI_Project.ViewModels
{
    public class QuestionairePageViewModel
    {
        public int OrderID { get; set; }
        public bool IsRandomised { get; set; }
        public OrderModel? Order{ get; set; }
        public List<(QuestionaireComponentViewModelBase viewModelBase, EComponentType componentType, EQuestionComponentType questionComponentType)> Items { get; set; }
        public RandomGroupViewModel RandomGroup { get; set; }
    }
}
