using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_ModelViews.Model.MessageModel
{
    public class SendMessageModel
    {
        public string ConversationId { get; set; }
        public string Content { get; set; }
        public string? MessageType { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
