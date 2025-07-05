using AI_Project.Models.UserModels.SubjectUserModelComponents;
using AI_Project.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace AI_Project.ViewModels
{
    public class SubjectUserViewModel
    {
        public Guid UserId { get; set; }
        public bool IsTreatmentGroup { get; set; }

        public List<AnswerViewModel> Answers { get; set; } = new List<AnswerViewModel>();
    }
}