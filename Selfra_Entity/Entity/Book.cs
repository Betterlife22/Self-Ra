using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class Book:BaseEntity
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Filepath { get; set; }
        public string? Format { get; set; }
        public TimeSpan TotalAudioDuration { get; set; }
    }
}
