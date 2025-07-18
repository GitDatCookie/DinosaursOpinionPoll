using AI_Project.Enums;
using AI_Project.ViewModels;

namespace AI_Project.Services.Interfaces
{
    public interface IEventNotificationService
    {
        public event Func<EQuestionComponentType, Task>? QuestionChanged;
        public event Func<EComponentType, Task>? ItemChanged;
        public event Action? ContainerReset;

        public void NotifyContainerReset();
        public void NotifyQuestionChanged(EQuestionComponentType questionType);
        public void NotifyItemChanged(EComponentType itemType);

    }
}
