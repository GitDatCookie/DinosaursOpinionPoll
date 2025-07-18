namespace AI_Project.Services
{
    using AI_Project.DBContext;
    using AI_Project.Models.UserModels.SubjectUserModelComponents;
    using AI_Project.Services.Interfaces;
    using AI_Project.ViewModels;
    using OpenAI;
    using OpenAI.Chat;
    using System.Threading.Tasks;

    public class AIService : IAIService
    {
        // OpenAI chat client and conversation history.
        public ChatClient Client { get; }
        private List<ChatMessage> conversationHistory = new List<ChatMessage>();
        private int conversationCount = 0;
        private const int ConversationWindow = 5;
        private readonly AI_ProjectDbContext _dbContext;
        private readonly IUserService _userService;

        public AIService(AI_ProjectDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
            var secretKey = Environment.GetEnvironmentVariable("API_KEY");
            if (string.IsNullOrWhiteSpace(secretKey))
            {
                throw new InvalidOperationException("Secret key is not configured.");
            }
            var api = new OpenAIClient(secretKey);
            Client = api.GetChatClient("gpt-4o");
        }

        // Get answer asynchronously and save each exchange.
        public async Task<(string, string?)> GetAnswerAsync(AIMessageViewModel userMessage)
        {
            string? summary = null;
            conversationHistory.Add(userMessage.Text);

            var response = await Client.CompleteChatAsync(conversationHistory);
            var answer = response.Value.Content.Select(x => x.Text).FirstOrDefault();

            conversationHistory.Add(new AssistantChatMessage(answer));
            conversationCount++;

            if (conversationCount >= ConversationWindow)
            {
                summary = await SummarizeConversationAsync();
                conversationCount = 0;
            }

            return ($"{answer}", summary);
        }

        // Summarize the conversation to keep the context manageable.
        // The summary replaces the full conversation history.
        private async Task<string> SummarizeConversationAsync()
        {
            // Aggregate the user and assistant messages into one text block.
            var conversationText = string.Join("\n",
                conversationHistory
                    .Where(m => m is UserChatMessage || m is AssistantChatMessage)
                    .Select(m => $"{m.GetType().Name}: {m.Content.Select(x => x.Text).FirstOrDefault()}"));

            // Build the summarization prompt with a clear system instruction and the conversation text.
            var summarizationMessages = new List<ChatMessage>
            {
                new SystemChatMessage("You are a summarization assistant. Please summarize the conversation below in a few concise sentences, highlighting the key points and context:"),
                new UserChatMessage(conversationText)
            };

            // Get the summary from the API.
            var summaryResponse = await Client.CompleteChatAsync(summarizationMessages);

            // Extract the text from the first choice.
            var summary = summaryResponse.Value.Content.Select(x => x.Text).FirstOrDefault()
                          ?? "Summary unavailable.";

            // Clear the conversation history and restart with the summarized context.
            conversationHistory.Clear();
            conversationHistory.Add(new SystemChatMessage($"Conversation summary so far: {summary}"));

            return summary;
        }

        public async Task CreateConversationAsync(AIConversationViewModel viewModel, List<string> summaries, Guid userId)
        {
            _dbContext.Add(ToModel(viewModel, summaries, userId));
            await _dbContext.SaveChangesAsync();
        }

        private async Task<AIConversationModel> ToModel(AIConversationViewModel viewModel, List<string> summaries, Guid userId)
        {
            var messageModelList = viewModel.Messages != null 
                ?[.. viewModel.Messages
                .Where(c => c.Text != null)
                .Select(message => new AIMessageModel
                {
                    MessageType = message.MessageType,
                    TimeStamp = message.TimeStamp,
                    Text = message.Text,
                })] : new List<AIMessageModel>();

            var user =  await _userService.GetUserModelAsync(userId);

            return new AIConversationModel
            {
                Messages = messageModelList,
                Summaries = summaries,
                User = user,
            };
        }
    }
}
