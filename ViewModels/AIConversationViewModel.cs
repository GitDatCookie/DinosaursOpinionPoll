using AI_Project.Models.UserModels;
using AI_Project.Models.UserModels.SubjectUserModelComponents;
using AI_Project.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace AI_Project.ViewModels
{
    public class AIConversationViewModel
    {
        public Guid Id { get; set; }
        public SubjectUserViewModel User { get; set; } = new();
        public ICollection<string> Summaries { get; set; } = [];
        public ICollection<AIMessageViewModel> Messages { get; set; } = [];
    }
}