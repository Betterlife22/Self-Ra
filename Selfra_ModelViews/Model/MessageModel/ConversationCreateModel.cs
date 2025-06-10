using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_ModelViews.Model.MessageModel
{
    public class ConversationCreateModel
    {
        public Guid SecondUserId { get; set; }
        public bool IsGroup { get; set; }
    }
}
