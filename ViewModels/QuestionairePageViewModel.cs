using AI_Project.Enums;
using AI_Project.Models.OrderModels;
using AI_Project.ViewModels.QuestionaireComponentViewModels;

namespace AI_Project.ViewModels
{
    public class QuestionairePageViewModel
    {
        public int OrderID { get; set; }
        public bool IsRandomised { get; set; }
        public List<(QuestionaireComponentViewModelBase, EItemType, EQuestionType)> Items { get; set; }
        public RandomGroupViewModel RandomGroup { get; set; }
    }
}
