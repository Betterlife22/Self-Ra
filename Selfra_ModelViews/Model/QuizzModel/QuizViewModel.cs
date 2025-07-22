using Selfra_Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_ModelViews.Model.QuizzModel
{
    public class QuizViewModel
    {
        public string Id { get; set; }

        public string? Title { get; set; }
        public virtual List<QuestionViewModel>? Questions { get; set; }

    }
}
