using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class QuizQuestion:BaseEntity
    {
        public string? QuizId { get; set; }
        public string? QuestionText { get; set; }

        public virtual Quiz? Quiz { get; set; }
        public virtual ICollection<QuizAnswer>? Answers { get; set; }
    }
}
