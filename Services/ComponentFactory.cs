using AI_Project.Enums;
using AI_Project.Models.QuestionaireComponentModels;
using AI_Project.Services.Interfaces;
using AI_Project.ViewModels.QuestionaireComponentViewModels;

namespace AI_Project.Services
{

    public class ComponentFactory : IComponentFactory
    {
        public QuestionaireComponentViewModelBase CreateComponent(QuestionaireComponentViewModelBase component)
        {
            switch (component)
            {
                case ImageViewModel image:
                    return image;
                case QuestionViewModel question:
                    return question;
                case FreeTextViewModel freeText:
                    return freeText;
                case VideoViewModel video:
                    return video;
                default:
                    return component;
            }
        }
    }
}
