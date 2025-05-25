using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class UserLessonProgress :BaseEntity
    {
        public Guid? UserId { get; set; }
        public string? LessonId { get; set; }
        public bool IsCompleted { get; set; }

        public virtual ApplicationUser? User { get; set; }
        public virtual Lesson? Lesson { get; set; }
    }
}
