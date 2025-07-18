using AI_Project.Enums;
using AI_Project.ViewModels.QuestionaireComponentViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Text.Json;

namespace AI_Project.Components.Pages.PageComponents.AttachmentComponents.Templates
{
    public partial class ImageTemplate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "BL0007:Use auto property", Justification = "Mapping to base.ViewModel is necessary.")]
        [Parameter, EditorRequired]
        public ImageViewModel TemplateViewModel
        {
            get
            {
                base.ViewModel ??= new ImageViewModel();
                return (ImageViewModel)base.ViewModel;
            }
            set => base.ViewModel = value;
        }

        private MudForm mudForm = null!;
        private IBrowserFile? selectedImageFile;

        protected override void OnInitialized()
        {
            IsEditable = TemplateViewModel.Id != Guid.Empty;
            Title = TemplateViewModel.Title;
            FilePath = TemplateViewModel.Url;
        }

        private async Task OnImageFileSelectedAsync(IBrowserFile imageFile)
        {
            selectedImageFile = imageFile;
            FilePath = await GetImagePreviewAsync(imageFile);
        }

        private static async Task<string> GetImagePreviewAsync(IBrowserFile file)
        {
            using var ms = new MemoryStream();
            await file.OpenReadStream(20 * 1024 * 1024)
                      .CopyToAsync(ms);
            var base64 = Convert.ToBase64String(ms.ToArray());
            return $"data:{file.ContentType};base64,{base64}";
        }

        private async Task UploadImageFileAsync(IBrowserFile imageFile)
        {
            var content = new MultipartFormDataContent();
            var fileStream = imageFile.OpenReadStream(20 * 1024 * 1024);
            var streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue(imageFile.ContentType);

            content.Add(streamContent, "imageFile", imageFile.Name);

            var absolutePath = navigationManager.BaseUri + "api/image/upload";
            var response = await httpClient.PostAsync(absolutePath, content);
            response.EnsureSuccessStatusCode();

            var dict = await response.Content
                .ReadFromJsonAsync<Dictionary<string, string>>(new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (dict != null && dict.TryGetValue("url", out var url))
            {
                FilePath = url;
            }
            else
            {
                FilePath = null;
            }
        }

        private async Task SaveImageAsync()
        {
            await mudForm.Validate();
            if (!mudForm.IsValid) return;

            if (selectedImageFile == null) return;

            await UploadImageFileAsync(selectedImageFile);

            ImageViewModel image = new()
            {
                Title = Title,
                Url = FilePath!,
                ItemType = EComponentType.Image
            };

            await SaveQuestionaireComponentModelAsync(image);
        }

        private async Task EditImageAsync()
        {
            TemplateViewModel.Title = Title;
            TemplateViewModel.Url = FilePath!;

            await EditQuestionaireComponentModelAsync(TemplateViewModel);
        }
    }
}