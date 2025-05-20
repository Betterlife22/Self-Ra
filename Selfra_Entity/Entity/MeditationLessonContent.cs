
using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class MeditationLessonContent :BaseEntity
    {
        public string? LessonId { get; set; }
        public string? Title { get; set; }
        public string? AudioUrl { get; set; }
        public string? Transcript { get; set; }
        public string? TextContent { get; set; }
        public string? ContentType { get; set; }
        public int Duration { get; set; }
        public bool IsFreePreview { get; set; }

        public Lesson? Lesson { get; set; }

    }
}
