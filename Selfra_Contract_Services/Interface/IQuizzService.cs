using Selfra_ModelViews.Model.QuizzModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Contract_Services.Interface
{
    public interface IQuizzService
    {
        Task Createquiz(QuizzModifyModel quizzModifyModel);
        Task TakeQuiz(QuizzSubmissionModel quizzSubmissionModel);
        Task<QuizViewModel> ListQuiz(string courseid);
        Task<QuizResultModel> GetUserQuizResult (string quizid);
    }
}
