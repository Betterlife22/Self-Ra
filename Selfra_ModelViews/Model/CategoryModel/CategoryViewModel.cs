﻿using Selfra_Entity.Model;
using Selfra_ModelViews.Model.CourseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_ModelViews.Model.CategoryModel
{
    public class CategoryViewModel
    {
        public string Id {  get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public string? CreatorName { get; set; }
        public virtual List<CourseViewModel>? Courses { get; set; }

    }
}
