using Selfra_Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Entity.Model
{
    public class Quiz :BaseEntity
    {
        public int CourseId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Title { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<QuizQuestion> Questions { get; set; }
    }
}
