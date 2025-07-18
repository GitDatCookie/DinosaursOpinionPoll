using AI_Project.Enums;
using MudBlazor;

namespace AI_Project.ViewModels.QuestionaireComponentViewModels.StyleComponentViewModels
{
    public class ComponentStyleViewModel
    {
        public string ComponentColour { get; set; } = "";
        public string Label { get; set; } = "";
        public string Placeholder { get; set; } = "";
        public string HelperText { get; set; } = "";
        public bool IsLabelColourised { get; set; }
        public Variant TextVariant { get; set; }
        public EQuestionAnswerFieldType QuestionAnswerFieldType { get; set; } = EQuestionAnswerFieldType.NoAnswerField;

        public NumericFieldStyleViewModel? NumericFieldStyle { get; set; }
    }
}
