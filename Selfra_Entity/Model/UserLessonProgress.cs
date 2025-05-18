using Selfra_Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Entity.Model
{
    public class UserLessonProgress :BaseEntity
    {
        public string UserId { get; set; }
        public int LessonId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? WatchedAt { get; set; }

        public ApplicationUser User { get; set; }
        public Lesson Lesson { get; set; }
    }
}
