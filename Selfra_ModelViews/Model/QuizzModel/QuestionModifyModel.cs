using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_ModelViews.Model.QuizzModel
{
    public class QuestionModifyModel
    {
        public string? QuestionText { get; set; }
        public List<AnswerModifyModel> Answers { get; set; }
    }
}
