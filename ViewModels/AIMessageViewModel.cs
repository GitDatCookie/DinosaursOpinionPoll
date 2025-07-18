using AI_Project.Enums;
using AI_Project.Models.UserModels.SubjectUserModelComponents;
using AI_Project.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace AI_Project.ViewModels
{
    public class AIMessageViewModel
    {
        public Guid Id { get; set; }

        public AIConversationViewModel Conversation { get; set; } = new();

        public EAIMessageType MessageType { get; set; }

        public string Text { get; set; } = "";
        public DateTime TimeStamp { get; set; }

    }
}