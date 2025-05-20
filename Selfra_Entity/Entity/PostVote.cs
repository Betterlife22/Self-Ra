using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class PostVote:BaseEntity
    {
        public string? UserId { get; set; }
        public string? PostId { get; set; }
        public int VoteValue { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public virtual Post? Post { get; set; }
    }
}
