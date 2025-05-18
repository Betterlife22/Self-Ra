using Selfra_Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Entity.Model
{
    public class Message :BaseEntity
    {
        public int ConversationId { get; set; }
        public Guid SenderId { get; set; }
        public string Content { get; set; }
        public string MessageType { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
        public virtual Conversation Conversation { get; set; }
        public virtual ApplicationUser Sender { get; set; }
    }
}
