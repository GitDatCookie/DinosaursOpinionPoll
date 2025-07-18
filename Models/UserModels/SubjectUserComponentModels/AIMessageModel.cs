using AI_Project.Enums;

namespace AI_Project.Models.UserModels.SubjectUserModelComponents
{
    //TODO
    public class AIMessageModel
    {
        public Guid Id { get; set; }

        public AIConversationModel Conversation { get; set; } = new();

        public EAIMessageType MessageType{ get; set; }

        public string Text { get; set; } = "";
        public DateTime TimeStamp { get; set; }

    }
}
