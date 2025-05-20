using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class UserNewsPreference :BaseEntity
    {
        public string? UserId { get; set; }
        public string? NewsCategoryId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public virtual NewsCategory? NewsCategory { get; set; }
    }
}
