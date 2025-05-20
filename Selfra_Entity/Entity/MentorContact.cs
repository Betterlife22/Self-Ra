using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class MentorContact:BaseEntity
    {
        public string? MentorId { get; set; }
        public string? UserId { get; set; }
        public string? Message { get; set; }
        public string? Status { get; set; }

        public virtual Mentor? Mentor { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
