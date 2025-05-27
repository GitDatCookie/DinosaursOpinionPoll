using AI_Project.Enums;
using AI_Project.Services.Interfaces;

namespace AI_Project.Services
{
    public class EventNotificationService : IEventNotificationService
    {
        public EventNotificationService()
        {
        }

        public event Action<EQuestionType>? QuestionChanged;
        public event Action<EItemType>? ItemChanged;
        public event Action? ContainerReset;

        public void NotifyContainerReset()
        {
            ContainerReset?.Invoke();
        }


        public void NotifyItemChanged(EItemType itemType)
        {
            ItemChanged?.Invoke(itemType);
        }

        public void NotifyQuestionChanged(EQuestionType questionType)
        {
            QuestionChanged?.Invoke(questionType);
        }
    }
}
