using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class ConversationParticipant : BaseEntity
    {
        public string? ConversationId { get; set; }
        public Guid? UserId { get; set; }
        public bool IsAdmin { get; set; }
        public virtual Conversation? Conversation { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}

