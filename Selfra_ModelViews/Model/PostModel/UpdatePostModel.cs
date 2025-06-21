

namespace Selfra_ModelViews.Model.PostModel
{
    public class UpdatePostModel
    {
        public string? PostId { get; set; }
        public Guid? UserId { get; set; }
        public string? Title { get; set; }
        public bool IsActive { get; set; }
        public string? Content { get; set; }
        public string? ArticleUrl { get; set; }

        public string? ImageUrl { get; set; }
    }
}
