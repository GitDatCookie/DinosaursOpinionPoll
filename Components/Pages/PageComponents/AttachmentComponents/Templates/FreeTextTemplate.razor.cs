using AI_Project.ViewModels.QuestionaireComponentViewModels;
using Microsoft.AspNetCore.Components;
using Tizzani.MudBlazor.HtmlEditor;

namespace AI_Project.Components.Pages.PageComponents.AttachmentComponents.Templates
{
    public partial class FreeTextTemplate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "BL0007:Use auto property", Justification = "Mapping to base.ViewModel is necessary.")]
        [Parameter, EditorRequired]
        public FreeTextViewModel TemplateViewModel
        {
            get
            {
                base.ViewModel ??= new FreeTextViewModel();
                return (FreeTextViewModel)base.ViewModel;
            }
            set => base.ViewModel = value;
        }

        private MudHtmlEditor editor = default!;

        protected override void OnInitialized()
        {
            if (TemplateViewModel != null && TemplateViewModel.Id != new Guid())
            {
                IsEditable = true;
                Title = TemplateViewModel.Title;
                FreeText = TemplateViewModel.FreeText;
            }
        }

        private async Task ResetEditorAsync()
        {
            await editor.Reset();
        }

        private async Task SaveTextAsync()
        {
            if (FreeText is null) return;

            SanitiseRichText(FreeText);

            FreeTextViewModel text = new()
            {
                Title = Title,
                FreeText = FreeText
            };

            await SaveQuestionaireComponentModelAsync(text);
        }

        private async Task EditTextAsync()
        {
            TemplateViewModel.Title = Title;

            await EditQuestionaireComponentModelAsync(TemplateViewModel);
        }
    }
}