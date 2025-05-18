using Selfra_Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Entity.Model
{
    public class PostVote:BaseEntity
    {
        public string UserId { get; set; }
        public int PostId { get; set; }
        public int VoteValue { get; set; }
        public DateTime VotedAt { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Post Post { get; set; }
    }
}
