using Selfra_Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Entity.Model
{
    public class QuizResult:BaseEntity
    {
        public int QuizId { get; set; }
        public Guid UserId { get; set; }
        public int Score { get; set; }
        public DateTime TakenAt { get; set; }

        public virtual Quiz Quiz { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
