using Selfra_Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Entity.Model
{
    public class SportsActivity:BaseEntity
    {
        public Guid UserId { get; set; }
        public string ActivityType { get; set; }
        public int Duration { get; set; }
        public float CaloriesBurned { get; set; }
        public string Notes { get; set; }
        public DateTime ActivityDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
