using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class QuizAnswer :BaseEntity
    {
        public string? QuestionId { get; set; }
        public string? AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public virtual QuizQuestion? Question { get; set; }

    }
}
