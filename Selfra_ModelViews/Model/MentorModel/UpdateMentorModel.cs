

namespace Selfra_ModelViews.Model.MentorModel
{
    public class UpdateMentorModel
    {
        public string? MentorId { get; set; }
        public string? Bio { get; set; }
        public string? ExpertiseAreas { get; set; }
        public float Rating { get; set; }
        public int TotalReviews { get; set; }
        public bool IsAvailable { get; set; }
    }
}
