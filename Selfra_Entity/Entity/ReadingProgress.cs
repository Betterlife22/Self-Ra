using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class ReadingProgress:BaseEntity
    {
        public Guid? UserId { get; set; }
        public string? BookId { get; set; }
        public int CurrentPage { get; set; }
        public TimeSpan CurrentPosition { get; set; }
        public bool IsFinished { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public virtual Book? Book { get; set; }
    }
}
