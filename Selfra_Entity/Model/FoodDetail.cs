using Selfra_Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Entity.Model
{
    public class FoodDetail :BaseEntity
    {
        public int LessonId { get; set; }
        public string FoodName { get; set; }
        public string Description { get; set; }
        public int Calories { get; set; }
        public string Country { get; set; }
        public string ImageUrl { get; set; }
        public string MealType { get; set; }
        public string Ingredients { get; set; }
        public virtual Lesson Lesson { get; set; }

    }
}
