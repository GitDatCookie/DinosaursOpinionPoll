namespace AI_Project.Models.UserModels.SubjectUserModelComponents
{
    //TODO
    public class AIConversationModel
    {
        public Guid Id { get; set; }
        public SubjectUserModel User { get; set; } = new();
        public ICollection<string> Summaries { get; set; } = [];
        public ICollection<AIMessageModel> Messages { get; set; } = [];
    }
}
