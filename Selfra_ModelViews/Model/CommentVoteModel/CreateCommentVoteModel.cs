

namespace Selfra_ModelViews.Model.CommentVoteModel
{
    public class CreateCommentVoteModel
    {
        public Guid? UserId { get; set; }
        public string? CommentId { get; set; }
        public int VoteValue { get; set; }
    }
}
