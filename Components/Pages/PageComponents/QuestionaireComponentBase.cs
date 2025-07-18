using AI_Project.Components.Pages.PageComponents.DialogueComponents;
using AI_Project.Enums;
using AI_Project.Services.Interfaces;
using AI_Project.ViewModels;
using AI_Project.ViewModels.QuestionaireComponentViewModels;
using Ganss.Xss;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;
using System.Net;
using System.Text.RegularExpressions;

namespace AI_Project.Components.Pages.PageComponents
{
    public class QuestionaireComponentBase : ComponentBase
    {
        [Inject] protected IDialogService DialogService { get; set; } = null!;
        [Inject] protected ISnackbar Snackbar { get; set; } = null!;
        [Inject] protected IQuestionService QuestionService { get; set; } = null!;
        [Inject] protected ITextService TextService { get; set; } = null!;
        [Inject] protected IImageService ImageService { get; set; } = null!;
        [Inject] protected IVideoService VideoService { get; set; } = null!;
        [Inject] protected IEventNotificationService EventNotificationService { get; set; } = null!;

        [Parameter] public EventCallback<(Guid questionID, EQuestionComponentType questionType)> OnQuestionDeleted { get; set; }
        [Parameter] public EventCallback<bool> OnItemEdited { get; set; }
        [Parameter] public EventCallback<EQuestionComponentType> OnQuestionChanged { get; set; }

        [Parameter] public QuestionaireComponentViewModelBase ViewModel { get; set; } = new();
        [Parameter] public SubjectUserViewModel UserViewModel { get; set; } = new();
        [Parameter] public bool IsEditable { get; set; } = false;

        //Primary base color if nothing else is set.
        public string ComponentColour { get; set; } = "rgb(89, 74, 226)";
        protected string Title { get; set; } = "";
        public List<AnswerViewModel> EditableOptions { get; set; } = [];
        public string? FreeText { get; set; }
        public string? FilePath { get; set; }
        public int? EditIndex { get; set; } = null;
        public bool IsRichText { get; set; } = false;
        public bool IsLabel { get; set; } = false;

        protected void FinishEditing()
        {
            EditIndex = null;
        }

        #region Editable Options
        protected void InitialiseOptions(string prefix, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                AddOption(prefix);
            }
        }

        protected void AddOption(string prefix)
        {
            var newIndex = EditableOptions.Count + 1;
            AnswerViewModel answer = new()
            {
                AnswerText = $"{prefix} {newIndex}",
                Question = (QuestionViewModel)ViewModel
            };
            EditableOptions.Add(answer);
        }

        protected void RemoveOption(int index)
        {
            if (index >= 0 && index < EditableOptions.Count)
            {
                EditableOptions.RemoveAt(index);

                if (EditIndex == index)
                {
                    EditIndex = null;
                }
            }
        }

        #endregion

        #region CRUD Operations
        protected async Task SaveQuestionaireComponentModelAsync(QuestionaireComponentViewModelBase questionaireComponent)
        {
            switch (questionaireComponent)
            {
                case QuestionViewModel question:
                    await QuestionService.CreateQuestionAsync(question);
                    Snackbar.Add("Question added.", Severity.Success);
                    EventNotificationService.NotifyQuestionChanged(question.QuestionType);
                    break;
                case FreeTextViewModel freeText:
                    TextService.CreateText(freeText);
                    Snackbar.Add("Text added.", Severity.Success);
                    EventNotificationService.NotifyItemChanged(EComponentType.FreeText);
                    break;
                case ImageViewModel image:
                    ImageService.CreateImage(image);
                    Snackbar.Add("Image added.", Severity.Success);
                    EventNotificationService.NotifyItemChanged(EComponentType.Image);
                    break;
                case VideoViewModel video:
                    await VideoService.CreateVideoAsync(video);
                    Snackbar.Add("Video added.", Severity.Success);
                    EventNotificationService.NotifyItemChanged(EComponentType.Video);
                    break;
                default:
                    Snackbar.Add("Unhandled component type.", Severity.Warning);
                    break;
            }
            EventNotificationService.NotifyContainerReset();
            await Task.CompletedTask;
        }
        protected async Task EditQuestionaireComponentModelAsync(QuestionaireComponentViewModelBase questionaireComponent)
        {
            switch (questionaireComponent)
            {
                case QuestionViewModel question:
                    await QuestionService.UpdateQuestionAsync(question.Id, question);
                    Snackbar.Add("Question changed.", Severity.Success);
                    EventNotificationService.NotifyQuestionChanged(question.QuestionType);
                    break;
                case FreeTextViewModel freeText:
                    TextService.ChangeText(freeText.Id, freeText);
                    Snackbar.Add("Text changed.", Severity.Success);
                    EventNotificationService.NotifyItemChanged(EComponentType.FreeText);
                    break;
                case ImageViewModel image:
                    ImageService.ChangeImage(image.Id, image);
                    Snackbar.Add("Image changed.", Severity.Success);
                    EventNotificationService.NotifyItemChanged(EComponentType.Image);
                    break;
                case VideoViewModel video:
                    await VideoService.UpdateVideoAsync(video.Id, video);
                    Snackbar.Add("Video changed.", Severity.Success);
                    EventNotificationService.NotifyItemChanged(EComponentType.Video);
                    break;
                default:
                    Snackbar.Add("Unhandled component type.", Severity.Warning);
                    break;
            }
            if (OnItemEdited.HasDelegate)
            {
                await OnItemEdited.InvokeAsync(false);
            }
            await Task.CompletedTask;
        }
        protected async Task DeleteQuestionaireComponentModelAsync(Guid questionaireComponentId, EComponentType itemType, EQuestionComponentType questionType)
        {
            switch (itemType)
            {
                case EComponentType.Question:
                    await QuestionService.DeleteQuestionAsync(questionaireComponentId);
                    Snackbar.Add("Question deleted", Severity.Success);
                    EventNotificationService.NotifyQuestionChanged(questionType);
                    break;
                case EComponentType.FreeText:
                    TextService.DeleteText(questionaireComponentId);
                    Snackbar.Add("Text deleted", Severity.Success);
                    EventNotificationService.NotifyItemChanged(itemType);
                    break;
                case EComponentType.Image:
                    ImageService.DeleteImage(questionaireComponentId);
                    Snackbar.Add("Image deleted", Severity.Success);
                    EventNotificationService.NotifyItemChanged(itemType);
                    break;
                case EComponentType.Video:
                    await VideoService.DeleteVideoAsync(questionaireComponentId);
                    Snackbar.Add("Video deleted", Severity.Success);
                    EventNotificationService.NotifyItemChanged(itemType);
                    break;
            }
            EventNotificationService.NotifyContainerReset();
            await Task.CompletedTask;
        }
        protected async Task ConfirmDeleteAsync(Guid questionaireComponentId, EComponentType itemType, EQuestionComponentType questionType)
        {
            var options = new DialogOptions
            {
                BackdropClick = false,
                MaxWidth = MaxWidth.Small,
                FullWidth = true,
                Position = DialogPosition.Center,
                CloseButton = true,
            };

            var dialogReference = await DialogService.ShowAsync<DeleteConfirmationDialogueComponent>("Confirm Deletion", options);
            var result = await dialogReference.Result;


            if (!result.Canceled)
            {
                // Delete the question and trigger event callbacks if provided.
                await DeleteQuestionaireComponentModelAsync(questionaireComponentId, itemType, questionType);
            }
            else
            {
                Snackbar.Add("Deletion cancelled", Severity.Info);
            }
        }
        #endregion

        protected async Task PickColourAsync()
        {
            var options = new DialogOptions
            {
                BackdropClick = false,
                MaxWidth = MaxWidth.ExtraSmall,
                FullWidth = true,
                Position = DialogPosition.Center,
                CloseButton = true,
            };

            var dialogReference = await DialogService.ShowAsync<ColourDialogueComponent>("Pick a colour", options);
            var result = await dialogReference.Result;

            if (result != null && !result.Canceled && result.Data is MudColor selectedColor)
            {
                ComponentColour = selectedColor.ToString();
            }
        }    
        
        protected async Task SetQuestionOptionsAsync(EQuestionAnswerFieldType questionAnswerFieldType, QuestionaireComponentViewModelBase viewModel)
        {
            var options = new DialogOptions
            {
                BackdropClick = false,
                MaxWidth = MaxWidth.ExtraSmall,
                FullWidth = true,
                Position = DialogPosition.Center,
                CloseButton = true,
            };

            var parameters = new DialogParameters
            {
                [nameof(TextOptionsDialogue.ViewModel)] = viewModel,
                [nameof(TextOptionsDialogue.AnswerFieldType)] = questionAnswerFieldType,
            };

            var dialogReference = await DialogService.ShowAsync<TextOptionsDialogue>("Customise your question look and feel",parameters, options);
            await dialogReference.Result;
            //if(result != null && !result.Canceled)
            //{
            //    viewModel = viewModel;
            //    StateHasChanged();
            //}

            switch (ViewModel.TitleFieldType)
            {
                case EComponentTitleFieldType.TextTitleField:
                    IsLabel = false;
                    IsRichText = false;
                    break;
                case EComponentTitleFieldType.RichTextTitleField:
                    IsLabel = false;
                    IsRichText = true;
                    break;
                case EComponentTitleFieldType.LabelAsTitleField:
                    IsLabel = true;
                    IsRichText = false;
                    break;
            }
            
        }

        protected void SanitiseRichText(string unsanitisedText)
        {
            var sanitiser = new HtmlSanitizer();
            sanitiser.AllowedAttributes.Add("class");
            sanitiser.Sanitize(unsanitisedText);
        }

        protected string CleanRichText(QuestionaireComponentViewModelBase item, string title)
        {
            if (item.TitleFieldType == EComponentTitleFieldType.RichTextTitleField && title is not null)
            {
                string regex = Regex.Replace(title, "<.*?>", string.Empty);
                string cleanedText = WebUtility.HtmlDecode(regex);
                title = cleanedText;
            }
            return title ?? string.Empty;
        }


        protected bool IsAnswerSelected(AnswerViewModel answer)
        {
            return UserViewModel?.Answers.Any(a => a.AnswerId == answer.AnswerId) ?? false;
        }

        // Helper: Adds or removes an answer in the user's Answer list based on selection.
        protected void UpdateUserSelection(AnswerViewModel answer, bool isSelected)
        {
            if (UserViewModel == null)
                return;

            if (isSelected)
            {
                if (!UserViewModel.Answers.Any(a => a.AnswerId == answer.AnswerId))
                    UserViewModel.Answers.Add(answer);
            }
            else
            {
                var existing = UserViewModel.Answers.FirstOrDefault(a => a.AnswerId == answer.AnswerId);
                if (existing != null)
                    UserViewModel.Answers.Remove(existing);
            }
        }

        protected void UpdateUserSelectionSingle(AnswerViewModel answer, bool isSelected)
        {
            if (UserViewModel == null)
                return;

            // Remove any answer that belongs to the same question.
            // It is assumed that AnswerViewModel has a property called QuestionId.
            UserViewModel.Answers.RemoveAll(a => a.Question.Id == answer.Question.Id);

            // In a single selection, if the user is selecting an answer, add it.
            if (isSelected)
            {
                UserViewModel.Answers.Add(answer);
            }
        }
    }
}
