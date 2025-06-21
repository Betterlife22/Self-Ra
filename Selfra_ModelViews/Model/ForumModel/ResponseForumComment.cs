

namespace Selfra_ModelViews.Model.ForumModel
{
    public class ResponseForumComment
    {
        public string? ForumCommentId { get; set; }
        public string? PostId { get; set; }
        public Guid? UserId { get; set; }
        public string? Content { get; set; }
    }
}
