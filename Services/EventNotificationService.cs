using AI_Project.Enums;
using AI_Project.Services.Interfaces;

namespace AI_Project.Services
{
    public class EventNotificationService : IEventNotificationService
    {
        public EventNotificationService()
        {
        }
        public event Func<EQuestionComponentType, Task>? QuestionChanged;
        public event Func<EComponentType, Task>? ItemChanged;
        public event Action? ContainerReset;

        public void NotifyContainerReset()
        {
            ContainerReset?.Invoke();
        }


        public void NotifyItemChanged(EComponentType itemType)
        {
            ItemChanged?.Invoke(itemType);
        }

        public void NotifyQuestionChanged(EQuestionComponentType questionType)
        {
            QuestionChanged?.Invoke(questionType);
        }
    }
}
