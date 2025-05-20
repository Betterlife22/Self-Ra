using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class CommentVote : BaseEntity
    {
        public Guid? UserId { get; set; }
        public string? CommentId { get; set; }
        public int VoteValue { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public virtual ForumComment? Comment { get; set; }
    }
}
