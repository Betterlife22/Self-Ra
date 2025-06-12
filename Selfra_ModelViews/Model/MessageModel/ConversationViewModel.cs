using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_ModelViews.Model.MessageModel
{
    public class ConversationViewModel
    {
        public string Id { get; set; }
        public string? ConversationName { get; set; }
        public bool IsGroup { get; set; }
        public string? LastMessage { get; set; }
        public string? LastSenderName { get; set; }
        public List<MessageViewModel> Messages { get; set; }
    }
}
