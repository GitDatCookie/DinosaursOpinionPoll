using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Project.Models.QuestionaireComponentModels
{
    public class FreeTextModel : QuestionaireComponentModelBase
    {
        [Required]
        public string FreeText { get; set; } = string.Empty;
    }
}
