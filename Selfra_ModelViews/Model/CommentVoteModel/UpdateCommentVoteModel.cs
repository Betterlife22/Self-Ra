

namespace Selfra_ModelViews.Model.CommentVoteModel
{
    public class UpdateCommentVoteModel
    {
        public string? CommentVoteId { get; set; }
        public Guid? UserId { get; set; }
        public string? CommentId { get; set; }
        public int VoteValue { get; set; }
    }
}
