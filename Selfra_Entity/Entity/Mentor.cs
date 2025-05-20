using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class Mentor :BaseEntity
    {
        public string? UserId { get; set; }
        public string? Bio { get; set; }
        public string? ExpertiseAreas { get; set; }
        public float Rating { get; set; }
        public int TotalReviews { get; set; }
        public bool IsAvailable { get; set; }

        public virtual ApplicationUser? User { get; set; }


    }
}
