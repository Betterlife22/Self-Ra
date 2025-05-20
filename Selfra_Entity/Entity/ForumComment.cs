using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class ForumComment :BaseEntity
    {
        public string? PostId { get; set; }
        public string? UserId { get; set; }
        public string? Content { get; set; }
        public virtual Post? Post { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
