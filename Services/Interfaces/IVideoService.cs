
using AI_Project.Models.QuestionaireComponentModels;
using AI_Project.ViewModels.QuestionaireComponentViewModels;

namespace AI_Project.Services.Interfaces
{
    public interface IVideoService
    {
        Task CreateVideoAsync(VideoViewModel video);
        Task DeleteVideoAsync(Guid videoId);
        Task UpdateVideoAsync(Guid videoId, VideoViewModel newVideoModel);
        Task<string> UploadVideoAsync(IFormFile videoFile);
        Task<VideoModel> GetVideoAsync(Guid videoId);
        Task<VideoViewModel> GetVideoViewModelAsync(Guid videoId);
        Task<List<VideoViewModel>> GetVideosAsync();
    }
}
