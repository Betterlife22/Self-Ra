using Selfra_Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Entity.Model
{
    public class Lesson : BaseEntity
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public int Duration { get; set; }
        public int OrderIndex { get; set; }
        public bool IsFreePreview { get; set; }
        public string LessonType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Course Course { get; set; }
        public ICollection<MeditationLessonContent> MeditationLessonContents { get; set; }
        public ICollection<FoodDetail> FoodDetails { get; set; }
    }
}
