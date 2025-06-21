

namespace Selfra_ModelViews.Model.CommentVoteModel
{
    public class ResponseCommentVoteModel
    {
        public string? CommentVoteId { get; set; }
        public Guid? UserId { get; set; }
        public string? CommentId { get; set; }
        public int VoteValue { get; set; }
    }
}
