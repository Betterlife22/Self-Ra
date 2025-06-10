using Selfra_Core.Base;
using System.Collections;

namespace Selfra_Entity.Model
{
    public class Conversation:BaseEntity
    {
        
        public string? ConversationName { get; set; }
        public bool IsGroup { get; set; }
        public string? LastMessage { get; set; }
        public string? LastSenderName { get; set; }
        public virtual ICollection<ConversationParticipant> Participants { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
