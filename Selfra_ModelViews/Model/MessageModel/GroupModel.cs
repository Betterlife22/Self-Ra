using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_ModelViews.Model.MessageModel
{
    public class GroupModel
    {
        public string ConversationName { get; set; }

        public List<Guid> GroupsMemberId { get; set; }
    }
}
