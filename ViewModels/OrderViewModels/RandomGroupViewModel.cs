using AI_Project.Enums;
using AI_Project.ViewModels;
using AI_Project.ViewModels.QuestionaireComponentViewModels;
using System.ComponentModel.DataAnnotations;

namespace AI_Project.Models.OrderModels
{
    public class RandomGroupViewModel
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public ERandomGroupType RandomGroupType { get; set; }

        public List<QuestionaireComponentViewModelBase> QuestionaireComponents{ get; set; }
        public List<QuestionairePageViewModel> Pages { get; set; }
    }
}
