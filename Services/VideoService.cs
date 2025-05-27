using AI_Project.DBContext;
using AI_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using AI_Project.Models.QuestionaireComponentModels;
using AI_Project.ViewModels.QuestionaireComponentViewModels;

namespace AI_Project.Services
{
    public class VideoService : IVideoService
    {
        private readonly AI_ProjectDbContext _dbContext;
        private readonly IWebHostEnvironment _env;

        public string userIdentifier = string.Empty;
        public VideoService(AI_ProjectDbContext dbContext, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _env = env;
        }

        public void CreateVideo(VideoViewModel video)
        {
            _dbContext.Add(CreateVideoModel(video));
            _dbContext.SaveChanges();
        }

        public void ChangeVideo(Guid videoId, VideoViewModel newImageViewModel)
        {
            VideoModel videoToBeChanged = GetVideo(videoId);
            videoToBeChanged.Title = newImageViewModel.Title;
            videoToBeChanged.Path = newImageViewModel.Path;

            _dbContext.SaveChanges();
        }

        public void DeleteVideo(Guid videoId)
        {
            _dbContext.Videos.Remove(GetVideo(videoId));
            _dbContext.SaveChanges();
        }

        public VideoViewModel GetVideoViewModel(Guid videoId)
        {
            VideoModel videoModel = _dbContext.Videos.FirstOrDefault(x => x.Id == videoId);
            return CreateViewModel(videoModel);
        }

        public List<VideoViewModel> GetVideos()
        {
            List<VideoViewModel> result = new();
            foreach (var video in _dbContext.Videos.ToList())
            {
                result.Add(CreateViewModel(video));
            }
            return result;
        }

        public async Task<string> UploadVideoAsync(IFormFile videoFile)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolder);

            var filePath = Path.Combine(uploadsFolder, videoFile.FileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await videoFile.CopyToAsync(stream);

            // Save the file path to the database
            var video = new VideoModel { Path = $"/uploads/{videoFile.FileName}" };

            _dbContext.Videos.Add(video);
            await _dbContext.SaveChangesAsync();

            return video.Path;
        }

        private VideoViewModel CreateViewModel(VideoModel videoModel)
        {
            // Depending on your implementation, you can add null-checks if necessary.
            return new VideoViewModel()
            {
                Id = videoModel.Id,
                Title = videoModel.Title,
                Path = videoModel.Path
            };
        }

        // Helper method to build an ImageModel from an ImageViewModel.
        private VideoModel CreateVideoModel(VideoViewModel videoViewModel)
        {
            return new VideoModel()
            {
                Title = videoViewModel.Title,
                Path = videoViewModel.Path
            };
        }

        // Retrieves an ImageModel from the database by its Id.
        private VideoModel GetVideo(Guid imageId)
        {
            return _dbContext.Videos.FirstOrDefault(x => x.Id == imageId);
        }
    }
}
