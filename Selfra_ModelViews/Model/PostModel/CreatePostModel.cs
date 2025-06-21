

namespace Selfra_ModelViews.Model.PostModel
{
    public class CreatePostModel
    {
        public Guid? UserId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? ArticleUrl { get; set; }

        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
