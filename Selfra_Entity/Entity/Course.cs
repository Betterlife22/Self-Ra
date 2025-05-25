using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class Course :BaseEntity
    {
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public string? AccessType { get; set; }
        public string? CategoryId { get; set; }
        public Guid? CreatorId { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? Level { get; set; }
        public int Duration { get; set; }
        public bool IsPublished { get; set; }
     

        public virtual Category? Category { get; set; }
        public virtual ApplicationUser? Creator { get; set; }
        public virtual ICollection<Lesson>? Lessons { get; set; }
        public virtual ICollection<Quiz>? Quizzes { get; set; }
    }
}
