using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class UserLessonProgress :BaseEntity
    {
        public string? UserId { get; set; }
        public string? LessonId { get; set; }
        public bool IsCompleted { get; set; }

        public ApplicationUser? User { get; set; }
        public Lesson? Lesson { get; set; }
    }
}
