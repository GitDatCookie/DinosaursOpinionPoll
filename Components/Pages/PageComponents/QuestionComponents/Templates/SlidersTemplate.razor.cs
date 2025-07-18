using AI_Project.Enums;
using AI_Project.ViewModels.QuestionaireComponentViewModels;
using AI_Project.ViewModels.QuestionaireComponentViewModels.StyleComponentViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AI_Project.Components.Pages.PageComponents.QuestionComponents.Templates
{
    public partial class SlidersTemplate
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


        private static int SliderMin { get; set; } = 2;
        private int SliderMax { get; set; } = 15;

        private int Value { get; set; } = 50;
        private MudForm mudForm = null!;
        private bool isSubmitted;
        private bool titleIsValid = false;

        private IEnumerable<string> SanitizedLabels => EditableOptions.Select(label => label.AnswerText.Replace(" ", "\u00A0"));

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
                InitialiseOptions("Label", 2);
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

        private async void SaveQuestion()
        {
            if (!await ValidateFieldsAsync()) return;

            QuestionViewModel question = new()
            {
                QuestionType = Enums.EQuestionComponentType.Slider,
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

            await EditQuestionaireComponentModelAsync(TemplateViewModel);
        }
    }
}