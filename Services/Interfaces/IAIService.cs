using AI_Project.Enums;
using AI_Project.ViewModels;
using OpenAI.Chat;
using OpenAI;

namespace AI_Project.Services.Interfaces
{
    public interface IAIService
    {
        public Task<string> GetAnswerAsync(string question);
    }
}
