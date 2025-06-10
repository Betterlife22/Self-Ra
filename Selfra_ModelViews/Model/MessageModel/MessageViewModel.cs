using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_ModelViews.Model.MessageModel
{
    public class MessageViewModel
    {
        public string? SenderName { get; set; }
        public string? Content { get; set; }
        public bool IsRead { get; set; }

    }
}
