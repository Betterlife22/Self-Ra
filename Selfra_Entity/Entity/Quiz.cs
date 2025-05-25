using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class Quiz :BaseEntity
    {
        public string? CourseId { get; set; }

        public string? Title { get; set; }
        public virtual Course? Course { get; set; }
        public virtual ICollection<QuizQuestion>? Questions { get; set; }
    }
}
