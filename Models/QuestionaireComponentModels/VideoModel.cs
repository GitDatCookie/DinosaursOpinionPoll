using System.ComponentModel.DataAnnotations;

namespace AI_Project.Models.QuestionaireComponentModels
{
    public class VideoModel : QuestionaireComponentModelBase
    {
        public string Path { get; set; } = null!;

    }
}
