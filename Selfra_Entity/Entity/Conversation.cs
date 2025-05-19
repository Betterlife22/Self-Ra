using Selfra_Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Entity.Model
{
    public class Conversation:BaseEntity
    {
        
        public bool IsGroup { get; set; }
        public DateTime? LastMessageAt { get; set; }
    }
}
