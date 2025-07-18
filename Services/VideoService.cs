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

        public async Task CreateVideoAsync(VideoViewModel video)
        {
            _dbContext.Add(CreateVideoModel(video));
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateVideoAsync(Guid videoId, VideoViewModel newImageViewModel)
        {
            VideoModel videoToBeChanged = await GetVideoAsync(videoId);
            videoToBeChanged.Title = newImageViewModel.Title;
            videoToBeChanged.Url = newImageViewModel.Url;

            _dbContext.SaveChanges();
        }

        public async Task DeleteVideoAsync(Guid videoId)
        {
            _dbContext.Videos.Remove(await GetVideoAsync(videoId));
            await _dbContext.SaveChangesAsync();
        }


        public async Task<VideoModel> GetVideoAsync(Guid imageId)
        {
           return await _dbContext.Videos.FirstOrDefaultAsync(x => x.Id == imageId);
        }

        public async Task<VideoViewModel> GetVideoViewModelAsync(Guid videoId)
        {
            VideoModel videoModel = await _dbContext.Videos.FirstOrDefaultAsync(x => x.Id == videoId);
            return CreateViewModel(videoModel);
        }

        public async Task<List<VideoViewModel>> GetVideosAsync()
        {
            List<VideoViewModel> result = [];
            foreach (var video in await _dbContext.Videos.ToListAsync())
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
            var video = new VideoModel { Url = $"/uploads/{videoFile.FileName}" };

            _dbContext.Videos.Add(video);
            await _dbContext.SaveChangesAsync();

            return video.Url;
        }

        private VideoViewModel CreateViewModel(VideoModel videoModel)
        {
            // Depending on your implementation, you can add null-checks if necessary.
            return new VideoViewModel()
            {
                Id = videoModel.Id,
                Title = videoModel.Title,
                Url = videoModel.Url
            };
        }

        // Helper method to build an ImageModel from an ImageViewModel.
        private VideoModel CreateVideoModel(VideoViewModel videoViewModel)
        {
            return new VideoModel()
            {
                Title = videoViewModel.Title,
                Url = videoViewModel.Url
            };
        }
    }
}
