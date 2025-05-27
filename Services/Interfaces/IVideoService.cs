
using AI_Project.ViewModels.QuestionaireComponentViewModels;

namespace AI_Project.Services.Interfaces
{
    public interface IVideoService
    {
        public void CreateVideo(VideoViewModel video);
        public void DeleteVideo(Guid videoId);
        public void ChangeVideo(Guid videoId, VideoViewModel newVideoModel);
        Task<string> UploadVideoAsync(IFormFile videoFile);
        public VideoViewModel GetVideoViewModel(Guid videoId);
        public List<VideoViewModel> GetVideos();
    }
}
