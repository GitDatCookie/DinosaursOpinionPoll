using AI_Project.DBContext;
using AI_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using AI_Project.Models.QuestionaireComponentModels;
using AI_Project.ViewModels.QuestionaireComponentViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AI_Project.Services
{
    public class ImageService : IImageService
    {
        private readonly AI_ProjectDbContext _dbContext;

        public string userIdentifier = string.Empty;
        public ImageService(AI_ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateImage(ImageViewModel image)
        {
            _dbContext.Add(CreateImageModel(image));
            _dbContext.SaveChanges();
        }

        public void ChangeImage(Guid imageId, ImageViewModel newImageViewModel)
        {
            ImageModel imageToBeChanged = GetImage(imageId);
            imageToBeChanged.Title = newImageViewModel.Title;
            imageToBeChanged.Url = newImageViewModel.Url;

            _dbContext.SaveChanges();
        }

        public void DeleteImage(Guid imageId)
        {
            _dbContext.Images.Remove(GetImage(imageId));
            _dbContext.SaveChanges();
        }
        public ImageModel GetImage(Guid imageId)
        {
            return _dbContext.Images.FirstOrDefault(x => x.Id == imageId);
        }
        public ImageViewModel GetImageViewModel(Guid imageId)
        {
            ImageModel imageModel = _dbContext.Images.FirstOrDefault(x => x.Id == imageId);
            return CreateViewModel(imageModel);
        }

        public List<ImageViewModel> GetImages()
        {
            List<ImageViewModel> result = new();
            foreach (var image in _dbContext.Images.ToList())
            {
                result.Add(CreateViewModel(image));
            }
            return result;
        }

        private ImageViewModel CreateViewModel(ImageModel imageModel)
        {
            return new ImageViewModel()
            {
                Id = imageModel.Id,
                Title = imageModel.Title,
                Url = imageModel.Url
            };
        }

        private ImageModel CreateImageModel(ImageViewModel imageViewModel)
        {
            return new ImageModel()
            {
                Title = imageViewModel.Title,
                Url = imageViewModel.Url
            };
        }

    }

}

