using AI_Project.Enums;
using AI_Project.ViewModels.QuestionaireComponentViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Text.Json;

namespace AI_Project.Components.Pages.PageComponents.AttachmentComponents.Templates
{
    public partial class VideoTemplate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "BL0007:Use auto property", Justification = "Mapping to base.ViewModel is necessary.")]
        [Parameter, EditorRequired]
        public VideoViewModel TemplateViewModel
        {
            get
            {
                base.ViewModel ??= new VideoViewModel();
                return (VideoViewModel)base.ViewModel;
            }
            set => base.ViewModel = value;
        }

        private MudForm mudForm = null!;
        private IBrowserFile? selectedVideoFile;

        protected override void OnInitialized()
        {
            if (TemplateViewModel != null && TemplateViewModel.Id != Guid.Empty)
            {
                IsEditable = true;
                Title = TemplateViewModel.Title;
                FilePath = TemplateViewModel.Url;
            }
        }

        private async Task OnVideoFileSelectedAsync(IBrowserFile file)
        {
            selectedVideoFile = file;
            FilePath = await GetVideoPreviewAsync(file);
        }

        private static async Task<string> GetVideoPreviewAsync(IBrowserFile file)
        {
            using var ms = new MemoryStream();
            await file.OpenReadStream(50 * 1024 * 1024)
                      .CopyToAsync(ms);

            var base64 = Convert.ToBase64String(ms.ToArray());
            return $"data:{file.ContentType};base64,{base64}";
        }

        private async Task UploadVideoFileAsync(IBrowserFile file)
        {
            var content = new MultipartFormDataContent();
            var stream = file.OpenReadStream(50 * 1024 * 1024);
            var streamContent = new StreamContent(stream);
            streamContent.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

            content.Add(streamContent, "videoFile", file.Name);

            var absoluePath = navigationManager.BaseUri + "api/video/upload";
            var response = await httpClient.PostAsync(absoluePath, content);
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

        private async Task SaveVideoAsync()
        {
            await mudForm.Validate();
            if (!mudForm.IsValid) return;

            if (selectedVideoFile == null) return;

            await UploadVideoFileAsync(selectedVideoFile);

            VideoViewModel video = new()
            {
                Title = Title,
                Url = FilePath!,
                ItemType = EComponentType.Video
            };

            await SaveQuestionaireComponentModelAsync(video);
        }

        private async Task EditVideoAsync()
        {
            TemplateViewModel.Title = Title;
            TemplateViewModel.Url = FilePath!;
            await EditQuestionaireComponentModelAsync(TemplateViewModel);
        }
    }
}