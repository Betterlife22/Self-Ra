﻿using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class Category:BaseEntity
    {
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public Guid? CreatorId { get; set; }
        public virtual ApplicationUser? Creator { get; set; }
        public virtual ICollection<Course>? Courses { get; set; }
    }
}
