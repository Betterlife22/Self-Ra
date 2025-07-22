using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_ModelViews.Model.LessonModel
{
    public class LessonViewModel
    {
        public string Id { get; set; }

        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public string? VideoUrl { get; set; }
        public int Duration { get; set; }
        public int OrderIndex { get; set; }
        public bool IsFreePreview { get; set; }
        public string? LessonType { get; set; }
    }
}
