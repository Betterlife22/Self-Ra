using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_ModelViews.Model.QuizzModel
{
    public class QuizzModifyModel
    {
        public string? CourseId { get; set; }
        public string? Title { get; set; }
        public List<QuestionModifyModel> Questions { get; set; }
    }
}
