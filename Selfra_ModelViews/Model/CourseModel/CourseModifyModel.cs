using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_ModelViews.Model.CourseModel
{
    public class CourseModifyModel
    {
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public string? AccessType { get; set; }
        public string? CategoryId { get; set; }
        public string? PackageId { get; set; }

        public Guid? CreatorId { get; set; }
        public IFormFile? ThumbnailUrl { get; set; } 
        public string? Level { get; set; }
        public int Duration { get; set; }
        public bool IsPublished { get; set; }
    }
}
