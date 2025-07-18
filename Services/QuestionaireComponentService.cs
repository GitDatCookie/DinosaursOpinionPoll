using AI_Project.DBContext;
using AI_Project.Enums;
using AI_Project.Models.QuestionaireComponentModels;
using AI_Project.Services.Interfaces;
using AI_Project.ViewModels.QuestionaireComponentViewModels;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace AI_Project.Services
{
    public class QuestionaireComponentService : IQuestionaireComponentService
    {
        private readonly IQuestionService _questionService;
        private readonly IVideoService _videoService;
        private readonly ITextService _textService;
        private readonly IImageService _imageService;
        public QuestionaireComponentService(IQuestionService questionService, IVideoService videoService, ITextService textService, IImageService imageService)
        {
            _questionService = questionService;
            _videoService = videoService;
            _textService = textService;
            _imageService = imageService;
        }
        public async Task UpdateQuestionaireComponentAsync((Guid questionaireComponentId, QuestionaireComponentViewModelBase viewModel, EComponentType itemType) questionaireComponent)
        {
            switch (questionaireComponent.itemType)
            {
                case EComponentType.Question:
                    if (questionaireComponent.viewModel is QuestionViewModel questionViewModel)
                    {
                        await _questionService.UpdateQuestionAsync(questionaireComponent.questionaireComponentId, questionViewModel);
                    }
                    else
                    {
                        throw new InvalidCastException("The viewModel is not a QuestionViewModel.");
                    }
                    break;
                case EComponentType.Video:
                    if (questionaireComponent.viewModel is VideoViewModel videoViewModel)
                    {
                        await _videoService.UpdateVideoAsync(questionaireComponent.questionaireComponentId, videoViewModel);
                    }
                    else
                    {
                        throw new InvalidCastException("The viewModel is not a VideoViewModel.");
                    }
                    break;
                case EComponentType.Image:
                    if (questionaireComponent.viewModel is ImageViewModel imageViewModel)
                    {
                        _imageService.ChangeImage(questionaireComponent.questionaireComponentId, imageViewModel);
                    }
                    else
                    {
                        throw new InvalidCastException("The viewModel is not a ImageViewModel.");
                    }
                    break;
                case EComponentType.FreeText:
                    if (questionaireComponent.viewModel is FreeTextViewModel textViewModel)
                    {
                        _textService.ChangeText(questionaireComponent.questionaireComponentId, textViewModel);
                    }
                    else
                    {
                        throw new InvalidCastException("The viewModel is not a FreeTextViewModel.");
                    }
                    break;
            }
        }
        public async Task CreateQuestionaireComponentAsync((QuestionaireComponentViewModelBase viewModel, EComponentType itemType) questionaireComponent)
        {
            switch (questionaireComponent.itemType)
            {
                case EComponentType.Question:
                    if (questionaireComponent.viewModel is QuestionViewModel questionViewModel)
                    {
                        await _questionService.CreateQuestionAsync(questionViewModel);
                    }
                    else
                    {
                        throw new InvalidCastException("The viewModel is not a QuestionViewModel.");
                    }
                    break;
                case EComponentType.Video:
                    if (questionaireComponent.viewModel is VideoViewModel videoViewModel)
                    {
                        await _videoService.CreateVideoAsync(videoViewModel);
                    }
                    else
                    {
                        throw new InvalidCastException("The viewModel is not a VideoViewModel.");
                    }
                    break;
                case EComponentType.Image:
                    if (questionaireComponent.viewModel is ImageViewModel imageViewModel)
                    {
                        _imageService.CreateImage(imageViewModel);
                    }
                    else
                    {
                        throw new InvalidCastException("The viewModel is not a ImageViewModel.");
                    }
                    break;
                case EComponentType.FreeText:
                    if (questionaireComponent.viewModel is FreeTextViewModel textViewModel)
                    {
                        _textService.CreateText(textViewModel);
                    }
                    else
                    {
                        throw new InvalidCastException("The viewModel is not a FreeTextViewModel.");
                    }
                    break;
            }
        }
        public async Task DeleteQuestionaireComponentAsync((Guid questionaireComponentId, EComponentType itemType) questionaireComponent)
        {
            switch (questionaireComponent.itemType)
            {
                case EComponentType.Question:
                    await _questionService.DeleteQuestionAsync(questionaireComponent.questionaireComponentId);
                    break;
                case EComponentType.Video:
                    await _videoService.DeleteVideoAsync(questionaireComponent.questionaireComponentId);
                    break;
                case EComponentType.Image:
                    _imageService.DeleteImage(questionaireComponent.questionaireComponentId);
                    break;
                case EComponentType.FreeText:
                    _textService.DeleteText(questionaireComponent.questionaireComponentId);
                    break;
            }
        }
        public async Task<QuestionaireComponentModelBase> GetQuestionaireComponentModelAsync((Guid questionaireComponentId, EComponentType itemType) questionaireComponent)
        {
            return questionaireComponent.itemType switch
            {
                EComponentType.Question => await _questionService.GetQuestionAsync(questionaireComponent.questionaireComponentId),
                EComponentType.Video => await _videoService.GetVideoAsync(questionaireComponent.questionaireComponentId),
                EComponentType.Image => _imageService.GetImage(questionaireComponent.questionaireComponentId),
                EComponentType.FreeText => _textService.GetText(questionaireComponent.questionaireComponentId),
                _ => throw new NotSupportedException($"The item type {questionaireComponent.itemType} is not supported."),
            };
        }
        public async Task<QuestionaireComponentViewModelBase> GetQuestionaireComponentViewModelAsync(QuestionaireComponentModelBase questionaireComponent)
        {
            return questionaireComponent switch
            {
                QuestionModel question => await _questionService.GetQuestionViewModelAsync(question.Id),
                VideoModel video => await _videoService.GetVideoViewModelAsync(video.Id),
                ImageModel image => _imageService.GetImageViewModel(image.Id),
                FreeTextModel freeText => _textService.GetTextViewModel(freeText.Id),
                _ => throw new NotSupportedException($"Component type {questionaireComponent.GetType().Name} is not supported.")
            };
        }
    }
}
