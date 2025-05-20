using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class Message :BaseEntity
    {
        public string? ConversationId { get; set; }
        public string? SenderId { get; set; }
        public string? Content { get; set; }
        public string? MessageType { get; set; }
        public bool IsRead { get; set; }
        public virtual Conversation? Conversation { get; set; }
        public virtual ApplicationUser? Sender { get; set; }
    }
}
