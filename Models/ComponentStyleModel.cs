using AI_Project.Enums;
using MudBlazor;

namespace AI_Project.Models
{
    public class ComponentStyleModel
    {
        public Guid Id { get; set; }
        public string? ComponentColour { get; set; }
        public string? Label { get; set; }
        public string? Placeholder { get; set; }
        public bool IsLabelColourised { get; set; }
        public string? HelperText { get; set; }
        public Variant TextVariant { get; set; }
        public EQuestionAnswerFieldType QuestionAnswerFieldType { get; set; } = EQuestionAnswerFieldType.NoAnswerField;

        public virtual NumberFieldStyleModel? NumberFieldStyle { get; set; }
    }
}
