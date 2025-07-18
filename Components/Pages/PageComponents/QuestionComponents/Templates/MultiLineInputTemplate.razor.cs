using AI_Project.Enums;
using AI_Project.ViewModels.QuestionaireComponentViewModels;
using AI_Project.ViewModels.QuestionaireComponentViewModels.StyleComponentViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AI_Project.Components.Pages.PageComponents.QuestionComponents.Templates
{
    public partial class MultiLineInputTemplate
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

        private MudForm mudForm = null!;
        private bool isSubmitted;
        private bool titleIsValid = false;

        private ComponentStyleViewModel CreateStyleModel() => new()
        {
            QuestionAnswerFieldType = EQuestionAnswerFieldType.AnswerField,
            ComponentColour = ComponentColour,
            IsLabelColourised = TemplateViewModel.ComponentStyle.IsLabelColourised,
            TextVariant = TemplateViewModel.ComponentStyle.TextVariant,
            HelperText = TemplateViewModel.ComponentStyle.HelperText,
            Label = TemplateViewModel.ComponentStyle.Label,
            Placeholder = TemplateViewModel.ComponentStyle.Placeholder
        };


        protected override void OnInitialized()
        {
            IsEditable = TemplateViewModel.Id != Guid.Empty;
            SetFieldFlags(TemplateViewModel.TitleFieldType);
            Title = TemplateViewModel.Title;
            ComponentColour = TemplateViewModel.ComponentStyle?.ComponentColour ?? ComponentColour;
        }

        private void SetFieldFlags(EComponentTitleFieldType type)
        {
            IsRichText = type == EComponentTitleFieldType.RichTextTitleField;
            IsLabel = type == EComponentTitleFieldType.LabelAsTitleField;
        }

        private void SetTitle(QuestionViewModel question)
        {
            switch (question.TitleFieldType)
            {
                case EComponentTitleFieldType.RichTextTitleField:
                    SanitiseRichText(question.Title);
                    break;
                case EComponentTitleFieldType.LabelAsTitleField:
                    question.Title = TemplateViewModel.ComponentStyle.Label;
                    break;
            }
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
                QuestionType = EQuestionComponentType.MultiLine,
                Title = Title,
                TitleFieldType = TemplateViewModel.TitleFieldType,
                ComponentStyle = CreateStyleModel()
            };

            SetTitle(question);

            await SaveQuestionaireComponentModelAsync(question);
        }

        private async Task EditQuestion()
        {
            if (!await ValidateFieldsAsync()) return;

            TemplateViewModel.Title = Title;
            TemplateViewModel.ComponentStyle = CreateStyleModel();

            SetTitle(TemplateViewModel);

            await EditQuestionaireComponentModelAsync(TemplateViewModel);
        }
    }
}