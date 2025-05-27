using AI_Project.Components.Pages.Components.DialogueComponents;
using AI_Project.Enums;
using AI_Project.Models.QuestionaireComponentModels;
using AI_Project.Services.Interfaces;
using AI_Project.ViewModels.QuestionaireComponentViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;
using System.ComponentModel;

namespace AI_Project.Components.Pages.Components
{
    public class QuestionaireComponentBase : ComponentBase
    {
        [Inject] protected IDialogService DialogService { get; set; }
        [Inject] protected ISnackbar Snackbar { get; set; }
        [Inject] protected IQuestionService QuestionService { get; set; }
        [Inject] protected ITextService TextService{ get; set; }
        [Inject] protected IImageService ImageService{ get; set; }
        [Inject] protected IVideoService VideoService{ get; set; }

        [Inject] protected IEventNotificationService EventNotificationService { get; set; }

        [Parameter] public EventCallback<(Guid questionID, EQuestionType questionType)> OnQuestionDeleted { get; set; }
        [Parameter] public EventCallback<bool> OnItemEdited { get; set; }
        [Parameter] public EventCallback<EQuestionType> OnQuestionChanged { get; set; }


        //Primary base color if nothing else is set.
        public string? ComponentColour { get; set; } = "rgb(89, 74, 226)";
        protected string Title { get; set; } = "";
        public List<string> EditableOptions { get; set; } = new List<string>();
        public string? FreeText { get; set; }
        public string? FilePath { get; set; }
        public int? EditIndex { get; set; } = null;
        public bool IsEditable { get; set; } = false;

        // Generic add-option method
        protected void AddOption(string prefix)
        {
            var newIndex = EditableOptions.Count + 1;
            EditableOptions.Add($"{prefix} {newIndex}");
        }

        // Generic remove-option method.
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

        // Finish editing—the same for both components.
        protected void FinishEditing()
        {
            EditIndex = null;
        }

        protected async Task ConfirmDeleteAsync(Guid questionaireComponentId, EItemType itemType, EQuestionType questionType)
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

        protected Task DeleteQuestionaireComponentModelAsync(Guid questionaireComponentId, EItemType itemType, EQuestionType questionType)
        {
            switch (itemType)
            {
                case EItemType.Question:
                    QuestionService.DeleteQuestion(questionaireComponentId);
                    Snackbar.Add("Question deleted", Severity.Success);
                    EventNotificationService.NotifyQuestionChanged(questionType);
                    break;
                case EItemType.FreeText:
                    TextService.DeleteText(questionaireComponentId);
                    Snackbar.Add("Text deleted", Severity.Success);
                    EventNotificationService.NotifyItemChanged(itemType);
                    break;
                case EItemType.Image:
                    ImageService.DeleteImage(questionaireComponentId);
                    Snackbar.Add("Image deleted", Severity.Success);
                    EventNotificationService.NotifyItemChanged(itemType);
                    break;
                case EItemType.Video:
                    VideoService.DeleteVideo(questionaireComponentId);
                    Snackbar.Add("Video deleted", Severity.Success);
                    EventNotificationService.NotifyItemChanged(itemType);
                    break;
            }
            EventNotificationService.NotifyContainerReset();
            return Task.CompletedTask;
        }

        protected Task SaveQuestionaireComponentModelAsync(QuestionaireComponentViewModelBase questionaireComponent)
        {
            switch (questionaireComponent)
            {
                case QuestionViewModel question:
                    QuestionService.CreateQuestion(question);
                    Snackbar.Add("Question added.", Severity.Success);
                    EventNotificationService.NotifyQuestionChanged(question.QuestionType);
                    break;
                case FreeTextViewModel freeText:
                    TextService.CreateText(freeText);
                    Snackbar.Add("Text added.", Severity.Success);
                    EventNotificationService.NotifyItemChanged(EItemType.FreeText);
                    break;
                case ImageViewModel image:
                    ImageService.CreateImage(image);
                    Snackbar.Add("Image added.", Severity.Success);
                    EventNotificationService.NotifyItemChanged(EItemType.Image);
                    break;
                case VideoViewModel video:
                    VideoService.CreateVideo(video);
                    Snackbar.Add("Video added.", Severity.Success);
                    EventNotificationService.NotifyItemChanged(EItemType.Video);
                    break;
                default:
                    Snackbar.Add("Unhandled component type.", Severity.Warning);
                    break;
            }
            EventNotificationService.NotifyContainerReset();
            return Task.CompletedTask;
        }

        protected async Task EditQuestionaireComponentModelAsync(QuestionaireComponentViewModelBase questionaireComponent)
        {
            switch (questionaireComponent)
            {
                case QuestionViewModel question:
                    QuestionService.ChangeQuestion(question.Id, question);
                    Snackbar.Add("Question changed.", Severity.Success);
                    EventNotificationService.NotifyQuestionChanged(question.QuestionType);
                    break;
                case FreeTextViewModel freeText:
                    TextService.ChangeText(freeText.Id, freeText);
                    Snackbar.Add("Text changed.", Severity.Success);
                    EventNotificationService.NotifyItemChanged(EItemType.FreeText);
                    break;
                case ImageViewModel image:
                    ImageService.ChangeImage(image.Id, image);
                    Snackbar.Add("Image changed.", Severity.Success);
                    EventNotificationService.NotifyItemChanged(EItemType.Image);
                    break;
                case VideoViewModel video:
                    VideoService.ChangeVideo(video.Id, video);
                    Snackbar.Add("Video changed.", Severity.Success);
                    EventNotificationService.NotifyItemChanged(EItemType.Video);
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

    }
}
