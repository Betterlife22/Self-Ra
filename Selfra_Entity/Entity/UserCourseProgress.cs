using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class UserCourseProgress:BaseEntity
    {
        public string? UserId { get; set; }
        public string? CourseId { get; set; }
        public float ProgressPercentage { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
        public ApplicationUser? User { get; set; }
        public Course? Course { get; set; }

    }
}
