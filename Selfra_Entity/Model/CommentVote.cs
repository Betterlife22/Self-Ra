using Selfra_Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Entity.Model
{
    public class CommentVote : BaseEntity
    {
        public Guid UserId { get; set; }
        public int CommentId { get; set; }
        public int VoteValue { get; set; }
        public DateTime VotedAt { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ForumComment Comment { get; set; }
    }
}
