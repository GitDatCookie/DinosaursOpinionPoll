using AI_Project.Enums;
using AI_Project.ViewModels.QuestionaireComponentViewModels;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Text.RegularExpressions;

namespace AI_Project.Components.Pages.PageComponents.DrawerComponents
{
    public partial class ItemDrawerComponent
    {
        [Parameter] public EventCallback<(QuestionaireComponentViewModelBase, EComponentType, EQuestionComponentType)> OnItemSelected { get; set; }

        [Parameter] public EQuestionComponentType QuestionType { get; set; }

        [Parameter] public EComponentType ItemType { get; set; }

        [Parameter] public string Icon { get; set; } = "";

        [GeneratedRegex("<.*?>", RegexOptions.Compiled)] private static partial Regex HtmlTagRegex();

        public List<QuestionaireComponentViewModelBase> ItemModel { get; set; } = [];

        private bool _isLoading = false;
        private static readonly SemaphoreSlim _loadLock = new(1, 1);

        protected override async Task OnInitializedAsync()
        {
            NotificationService.QuestionChanged -= OnQuestionChangedAsync;
            NotificationService.QuestionChanged += OnQuestionChangedAsync;

            NotificationService.ItemChanged -= OnItemChangedAsync;
            NotificationService.ItemChanged += OnItemChangedAsync;

            await LoadQuestionaireItems();
        }

        public async Task ReloadAsync()
        {
            await LoadQuestionaireItems();
            await InvokeAsync(StateHasChanged);
        }

        public string GeneratePreview(QuestionaireComponentViewModelBase item)
        {
            string titleField = CleanRichText(item);

            return titleField.Length > 33
            ? $"{titleField[..32]} ..."
            : titleField;
        }

        public string CleanRichText(QuestionaireComponentViewModelBase item)
        {
            string titleField = item.Title;
            if (item.TitleFieldType == EComponentTitleFieldType.RichTextTitleField)
            {
                string regex = HtmlTagRegex().Replace(titleField, string.Empty);
                string cleanedText = WebUtility.HtmlDecode(regex);
                titleField = cleanedText;
            }
            return titleField;
        }

        public async Task LoadQuestionaireItems()
        {
            if (_isLoading) return;

            _isLoading = true;
            try
            {
                await _loadLock.WaitAsync();

                switch (ItemType)
                {
                    case EComponentType.Image:
                        ItemModel = [.. imageService.GetImages().Cast<QuestionaireComponentViewModelBase>()];
                        break;
                    case EComponentType.Video:
                        var videos = await videoService.GetVideosAsync();
                        ItemModel = [.. videos.Cast<QuestionaireComponentViewModelBase>()];
                        break;
                    case EComponentType.FreeText:
                        ItemModel = [.. textService.GetTexts().Cast<QuestionaireComponentViewModelBase>()];
                        break;
                    case EComponentType.Question:
                        var questions = await questionService.GetQuestionsByTypeAsync(QuestionType);
                        ItemModel = [.. questions.Cast<QuestionaireComponentViewModelBase>()];
                        break;
                    default:
                        ItemModel = [];
                        break;
                }
            }
            finally
            {
                _loadLock.Release();
                _isLoading = false;
            }
        }

        private async Task OnQuestionChangedAsync(EQuestionComponentType questionType)
        {
            if (questionType != this.QuestionType) return;

            await ReloadAsync();
        }

        private async Task OnItemChangedAsync(EComponentType itemType)
        {

            if (itemType != this.ItemType) return;

            await ReloadAsync();
        }
    }
}