using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class QuizResult:BaseEntity
    {
        public string? QuizId { get; set; }
        public string? UserId { get; set; }
        public int Score { get; set; }

        public virtual Quiz? Quiz { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
