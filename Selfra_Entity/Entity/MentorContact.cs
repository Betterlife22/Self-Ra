using Selfra_Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Entity.Model
{
    public class MentorContact:BaseEntity
    {
        public int MentorId { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public DateTime RequestedAt { get; set; }

        public virtual Mentor Mentor { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
