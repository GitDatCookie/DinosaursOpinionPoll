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
        public async Task UpdateQuestionaireComponentAsync((Guid questionaireComponentId, QuestionaireComponentViewModelBase viewModel, EItemType itemType) questionaireComponent)
        {
            switch (questionaireComponent.itemType)
            {
                case EItemType.Question:
                    if (questionaireComponent.viewModel is QuestionViewModel questionViewModel)
                    {
                        await _questionService.UpdateQuestionAsync(questionaireComponent.questionaireComponentId, questionViewModel);
                    }
                    else
                    {
                        throw new InvalidCastException("The viewModel is not a QuestionViewModel.");
                    }
                    break;
                case EItemType.Video:
                    if (questionaireComponent.viewModel is VideoViewModel videoViewModel)
                    {
                        _videoService.ChangeVideo(questionaireComponent.questionaireComponentId, videoViewModel);
                    }
                    else
                    {
                        throw new InvalidCastException("The viewModel is not a VideoViewModel.");
                    }
                    break;
                case EItemType.Image:
                    if (questionaireComponent.viewModel is ImageViewModel imageViewModel)
                    {
                        _imageService.ChangeImage(questionaireComponent.questionaireComponentId, imageViewModel);
                    }
                    else
                    {
                        throw new InvalidCastException("The viewModel is not a ImageViewModel.");
                    }
                    break;
                case EItemType.FreeText:
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
        public async Task CreateQuestionaireComponentAsync((QuestionaireComponentViewModelBase viewModel, EItemType itemType) questionaireComponent)
        {
            switch (questionaireComponent.itemType)
            {
                case EItemType.Question:
                    if (questionaireComponent.viewModel is QuestionViewModel questionViewModel)
                    {
                        await _questionService.CreateQuestionAsync(questionViewModel);
                    }
                    else
                    {
                        throw new InvalidCastException("The viewModel is not a QuestionViewModel.");
                    }
                    break;
                case EItemType.Video:
                    if (questionaireComponent.viewModel is VideoViewModel videoViewModel)
                    {
                        _videoService.CreateVideo(videoViewModel);
                    }
                    else
                    {
                        throw new InvalidCastException("The viewModel is not a VideoViewModel.");
                    }
                    break;
                case EItemType.Image:
                    if (questionaireComponent.viewModel is ImageViewModel imageViewModel)
                    {
                        _imageService.CreateImage(imageViewModel);
                    }
                    else
                    {
                        throw new InvalidCastException("The viewModel is not a ImageViewModel.");
                    }
                    break;
                case EItemType.FreeText:
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
        public async Task DeleteQuestionaireComponentAsync((Guid questionaireComponentId, EItemType itemType) questionaireComponent)
        {
            switch (questionaireComponent.itemType)
            {
                case EItemType.Question:
                    await _questionService.DeleteQuestionAsync(questionaireComponent.questionaireComponentId);
                    break;
                case EItemType.Video:
                    _videoService.DeleteVideo(questionaireComponent.questionaireComponentId);
                    break;
                case EItemType.Image:
                    _imageService.DeleteImage(questionaireComponent.questionaireComponentId);
                    break;
                case EItemType.FreeText:
                    _textService.DeleteText(questionaireComponent.questionaireComponentId);
                    break;
            }
        }
        public async Task<QuestionaireComponentModelBase> GetQuestionaireComponentModelAsync((Guid questionaireComponentId, EItemType itemType) questionaireComponent)
        {
            switch (questionaireComponent.itemType)
            {
                case EItemType.Question:
                    return await _questionService.GetQuestionAsync(questionaireComponent.questionaireComponentId);
                case EItemType.Video:
                    return _videoService.GetVideo(questionaireComponent.questionaireComponentId);
                case EItemType.Image:
                    return _imageService.GetImage(questionaireComponent.questionaireComponentId);
                case EItemType.FreeText:
                    return _textService.GetText(questionaireComponent.questionaireComponentId);
                default:
                    throw new NotSupportedException($"The item type {questionaireComponent.itemType} is not supported.");
            }
        }
        public async Task<QuestionaireComponentViewModelBase> GetQuestionaireComponentViewModelAsync(QuestionaireComponentModelBase questionaireComponent)
        {
            return questionaireComponent switch
            {
                QuestionModel question => await _questionService.GetQuestionViewModelAsync(question.Id),
                VideoModel video => _videoService.GetVideoViewModel(video.Id),
                ImageModel image => _imageService.GetImageViewModel(image.Id),
                FreeTextModel freeText => _textService.GetTextViewModel(freeText.Id),
                _ => throw new NotSupportedException($"Component type {questionaireComponent.GetType().Name} is not supported.")
            };
        }
    }
}
