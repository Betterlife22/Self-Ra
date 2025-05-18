using Selfra_Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Entity.Model
{
    public class UserCourseProgress:BaseEntity
    {
        public Guid UserId { get; set; }
        public int CourseId { get; set; }
        public float ProgressPercentage { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public ApplicationUser User { get; set; }
        public Course Course { get; set; }

    }
}
