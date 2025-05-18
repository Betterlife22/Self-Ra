using Selfra_Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Entity.Model
{
    public class ConversationParticipant : BaseEntity
    {
        public int ConversationId { get; set; }
        public string UserId { get; set; }
        public DateTime JoinedAt { get; set; }
        public bool IsAdmin { get; set; }
        public virtual Conversation Conversation { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}

