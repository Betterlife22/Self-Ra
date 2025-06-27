using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class Post:BaseEntity
    {
        public Guid? UserId { get; set; }
        public string? Title { get; set; }

        public string? ArticleUrl { get; set; }

        public string? ImageUrl { get; set; }
        public string? Content { get; set; }

        public string? CategoryPost { get; set; }
        public bool IsActive { get; set; }
        public virtual ApplicationUser? User { get; set; }

    }
}
