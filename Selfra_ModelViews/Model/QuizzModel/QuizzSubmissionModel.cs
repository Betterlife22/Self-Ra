using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_ModelViews.Model.QuizzModel
{
    public class QuizzSubmissionModel
    {
        public string UserId { get; set; }
        public string QuizId { get; set; }
        public List<SubmittedAnswerModel> SubmittedAnswers { get; set; }
    }
}
