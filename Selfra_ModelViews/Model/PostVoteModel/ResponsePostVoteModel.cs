
namespace Selfra_ModelViews.Model.PostVoteModel
{
    public class ResponsePostVoteModel
    {
        public string? PostVoteId { get; set; }
        public Guid? UserId { get; set; }
        public string? PostId { get; set; }
        public int VoteValue { get; set; }
    }
}
