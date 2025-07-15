using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_ModelViews.Model.ProgressModel
{
    public class LessonProgressViewModel
    {
        public string? LessonId { get; set; }
        public int OrderIndex { get; set; }
        public string? LessonName { get; set; }

        public bool IsCompleted { get; set; }
    }
}
