namespace AI_Project.Services
{
    using AI_Project.Services.Interfaces;
    using OpenAI;
    using OpenAI.Chat;
    public class AIService : IAIService
    {
        // OpenAI chat client and conversation history.
        public ChatClient Client { get; }
        private List<ChatMessage> conversationHistory = new List<ChatMessage>();
        private int conversationCount = 0;
        private const int ConversationWindow = 5;
        

        public AIService(IConfiguration configuration)
        {
            var secretKey = configuration["ChatGptKey"];
            if (string.IsNullOrWhiteSpace(secretKey))
            {
                throw new InvalidOperationException("Secret key is not configured.");
            }
            var api = new OpenAIClient(secretKey);
            Client = api.GetChatClient("gpt-4o");
        }

        // Get answer asynchronously and save each exchange.
        public async Task<string> GetAnswerAsync(string question)
        {
            // Add the user's message to the conversation history.
            conversationHistory.Add(new UserChatMessage(question));

            // Get the assistant's answer with the current conversation context.
            var response = await Client.CompleteChatAsync(conversationHistory);
            var answer = response.Value.Content.Select(x => x.Text).FirstOrDefault();

            // Append the assistant's answer to the conversation history.
            conversationHistory.Add(new AssistantChatMessage(answer));

            // Increase the conversation counter (each exchange counts as one).
            conversationCount++;

            // Save the current Q&A pair in your database.
            //var exchange = new ChatExchange
            //{
            //    Question = question,
            //    Answer = answer,
            //    Timestamp = DateTime.UtcNow
            //};
            //_chatContext.ChatExchanges.Add(exchange);
            //await _chatContext.SaveChangesAsync();

            // If the conversation window has been reached, summarize and reset the context.
            if (conversationCount >= ConversationWindow)
            {
                await SummarizeConversationAsync();
                conversationCount = 0;
            }

            return $"{answer}";
        }

        // Summarize the conversation to keep the context manageable.
        // The summary replaces the full conversation history.
        private async Task SummarizeConversationAsync()
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
        }

    }
}
