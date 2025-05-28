using Selfra_Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_ModelViews.Model.QuizzModel
{
    public class QuestionViewModel
    {
        public string? QuestionText { get; set; }
        public virtual List<AnswerViewModel>? Answers { get; set; }

    }
}
