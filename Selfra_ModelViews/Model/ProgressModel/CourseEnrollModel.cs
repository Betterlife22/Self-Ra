using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_ModelViews.Model.ProgressModel
{
    public class CourseEnrollModel
    {
        public string? CourseId { get; set; }
        public float ProgressPercentage { get; set; } = 0;
        public bool IsCompleted { get; set; } = false;
    }
}
