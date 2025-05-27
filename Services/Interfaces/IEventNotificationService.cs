using AI_Project.Enums;
using AI_Project.ViewModels;

namespace AI_Project.Services.Interfaces
{
    public interface IEventNotificationService
    {
        public event Action<EQuestionType>? QuestionChanged;
        public event Action<EItemType>? ItemChanged;
        public event Action? ContainerReset;

        public void NotifyContainerReset();
        public void NotifyQuestionChanged(EQuestionType questionType);
        public void NotifyItemChanged(EItemType itemType);

    }
}
