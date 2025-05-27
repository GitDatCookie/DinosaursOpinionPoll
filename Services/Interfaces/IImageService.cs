
using AI_Project.ViewModels.QuestionaireComponentViewModels;

namespace AI_Project.Services.Interfaces
{
    public interface IImageService
    {
        public void CreateImage(ImageViewModel image);
        public void DeleteImage(Guid imageId);
        public void ChangeImage(Guid imageId, ImageViewModel newImageViewModel);
        public ImageViewModel GetImageViewModel(Guid imageId);
        public List<ImageViewModel> GetImages();
    }
}
