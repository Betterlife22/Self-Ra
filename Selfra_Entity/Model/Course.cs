using Selfra_Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Entity.Model
{
    public class Course :BaseEntity
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string AccessType { get; set; }
        public int CategoryId { get; set; }
        public Guid CreatorId { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Level { get; set; }
        public int Duration { get; set; }
        public bool IsPublished { get; set; }
     

        public Category Category { get; set; }
        public ApplicationUser Creator { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
        public ICollection<Quiz> Quizzes { get; set; }
    }
}
