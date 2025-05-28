using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_ModelViews.Model.ProgressModel
{
    public class CourseProgessViewModel
    {
        public string? CourseName { get; set; }
        public float ProgressPercentage { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
        public List<LessonProgressViewModel> Lessons { get; set; } 
    }
}
