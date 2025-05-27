using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_ModelViews.Model.ProgressModel
{
    public class LessonStartModel
    {
        public string? LessonId { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
