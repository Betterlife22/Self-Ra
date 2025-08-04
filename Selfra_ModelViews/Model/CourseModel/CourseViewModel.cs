using Selfra_Entity.Model;
using Selfra_ModelViews.Model.LessonModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_ModelViews.Model.CourseModel
{
    public class CourseViewModel
    {
        [Key]
        public string Id { get; set; }
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public string? AccessType { get; set; }
        public string? CategoryName { get; set; }
        public string? CreatorName { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? Level { get; set; }
        public int Duration { get; set; }
        public bool IsPublished { get; set; }
        public  List<LessonViewModel>? Lessons { get; set; }

    }
}
