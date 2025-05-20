using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class SportsActivity:BaseEntity
    {
        public string? UserId { get; set; }
        public string? ActivityType { get; set; }
        public int Duration { get; set; }
        public float CaloriesBurned { get; set; }
        public string? Notes { get; set; }
        public virtual ApplicationUser? User { get; set; }

    }
}
