using AI_Project.Enums;
using MudBlazor;

namespace AI_Project.Models.QuestionaireComponentModels.StyleComponents
{
    public class NumberFieldStyleModel
    {
        public Guid Id { get; set; }
        public ENumberType NumberType { get; set; }

        public float? MinNumberFloat { get; set; }
        public float? MaxNumberFloat { get; set; }        
        public int? MinNumberInteger { get; set; }
        public int? MaxNumberInteger { get; set; }
    }
}
