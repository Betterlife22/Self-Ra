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
        public string? CreatorId { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? Level { get; set; }
        public int Duration { get; set; }
        public bool IsPublished { get; set; }
     

        public Category? Category { get; set; }
        public ApplicationUser? Creator { get; set; }
        public ICollection<Lesson>? Lessons { get; set; }
        public ICollection<Quiz>? Quizzes { get; set; }
    }
}
