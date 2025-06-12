

namespace Selfra_ModelViews.Model.PostVoteModel
{
    public class CreatePostVoteModel
    {
        public Guid? UserId { get; set; }
        public string? PostId { get; set; }
        public int VoteValue { get; set; }
    }
}
