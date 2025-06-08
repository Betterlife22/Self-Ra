

namespace Selfra_ModelViews.Model.MentorModel
{
    public class CreateMentorModel
    {
        public Guid? UserId { get; set; }
        public string? Bio { get; set; }
        public string? ExpertiseAreas { get; set; }
        public float Rating { get; set; }
        public int TotalReviews { get; set; }
        public bool IsAvailable { get; set; }
    }
}
