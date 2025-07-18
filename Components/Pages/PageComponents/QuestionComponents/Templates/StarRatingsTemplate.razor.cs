using AI_Project.Enums;
using AI_Project.ViewModels.QuestionaireComponentViewModels;
using AI_Project.ViewModels.QuestionaireComponentViewModels.StyleComponentViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AI_Project.Components.Pages.PageComponents.QuestionComponents.Templates
{
    public partial class StarRatingsTemplate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "BL0007:Use auto property", Justification = "Mapping to base.ViewModel is necessary.")]
        [Parameter, EditorRequired]
        public QuestionViewModel TemplateViewModel
        {
            get
            {
                base.ViewModel ??= new QuestionViewModel();
                return (QuestionViewModel)base.ViewModel;
            }
            set => base.ViewModel = value;
        }

        private int selectedVal = 0;

        private int RatingValue
        {
            get => selectedVal + 1;
            set => selectedVal = value - 1;
        }

        private int? activeVal;

        private const int MinStars = 2;
        private const int MaxStars = 10;
        private MudForm mudForm = null!;
        private bool isSubmitted;
        private bool titleIsValid = false;

        private string LabelText =>
            (activeVal ?? selectedVal) is int idx && idx >= 0 && idx < EditableOptions.Count
                ? EditableOptions[idx].AnswerText
                : "Rate our product!";

        private ComponentStyleViewModel CreateStyleModel() => new()
        {
            QuestionAnswerFieldType = EQuestionAnswerFieldType.NoAnswerField,
            ComponentColour = ComponentColour,
        };

        protected override void OnInitialized()
        {
            IsEditable = TemplateViewModel.Id != Guid.Empty;
            SetFieldFlags(TemplateViewModel.TitleFieldType);
            Title = TemplateViewModel.Title;
            ComponentColour = TemplateViewModel.ComponentStyle?.ComponentColour ?? ComponentColour;
            if (TemplateViewModel.Answers is not null)
            {
                EditableOptions = TemplateViewModel.Answers;
            }
            else
            {
                InitialiseOptions("Star", 3);
            }
        }

        private void SetFieldFlags(EComponentTitleFieldType type)
        {
            IsRichText = type == EComponentTitleFieldType.RichTextTitleField;
        }

        private async Task<bool> ValidateFieldsAsync()
        {
            isSubmitted = true;
            await mudForm.Validate();

            titleIsValid = !string.IsNullOrWhiteSpace(CleanRichText(TemplateViewModel, Title));

            return (mudForm.IsValid && titleIsValid);
        }

        private void HandleHoveredValueChanged(int? val)
        {
            if (val.HasValue)
                activeVal = val.Value - 1;
            else
                activeVal = null;
        }

        private async void SaveQuestion()
        {
            if (!await ValidateFieldsAsync()) return;

            QuestionViewModel question = new()
            {
                QuestionType = Enums.EQuestionComponentType.StarRating,
                Title = Title,
                Answers = EditableOptions,
                TitleFieldType = TemplateViewModel.TitleFieldType,
                ComponentStyle = CreateStyleModel()
            };

            if (TemplateViewModel.TitleFieldType == EComponentTitleFieldType.RichTextTitleField)
                SanitiseRichText(TemplateViewModel.Title);

            await SaveQuestionaireComponentModelAsync(question);

        }

        private async Task EditQuestion()
        {
            if (!await ValidateFieldsAsync()) return;

            TemplateViewModel.Title = Title;
            TemplateViewModel.Answers = EditableOptions;
            TemplateViewModel.ComponentStyle = CreateStyleModel();

            if (TemplateViewModel.TitleFieldType == EComponentTitleFieldType.RichTextTitleField)
                SanitiseRichText(TemplateViewModel.Title);

            await EditQuestionaireComponentModelAsync(TemplateViewModel);

        }
    }
}