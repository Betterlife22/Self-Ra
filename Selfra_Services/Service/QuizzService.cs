using AutoMapper;
using Microsoft.AspNetCore.Http;
using Selfra_Contract_Services.Interface;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.QuizzModel;
using Selfra_Services.Infrastructure;
using Selft.Contract.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Services.Service
{
    public class QuizzService : IQuizzService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QuizzService(IUnitOfWork unitOfWork,IMapper mapper,IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task Createquiz(QuizzModifyModel quizzModifyModel)
        {
            var quiz = new Quiz
            {
                CourseId = quizzModifyModel.CourseId,
                Title = quizzModifyModel.Title,
                Questions = quizzModifyModel.Questions.Select(q => new QuizQuestion
                {
                    QuestionText = q.QuestionText,
                    Answers = q.Answers.Select(a => new QuizAnswer
                    {
                        AnswerText = a.AnswerText,
                        IsCorrect = a.IsCorrect
                    }).ToList()
                }).ToList()
            };

            await _unitOfWork.GetRepository<Quiz>().AddAsync(quiz);
            await _unitOfWork.SaveAsync();

        }

        public async Task<QuizResultModel> GetUserQuizResult(string quizid)
        {
            var userId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);
            var quizresult = await _unitOfWork.GetRepository<QuizResult>().GetByPropertyAsync(
                qr => qr.QuizId == quizid && qr.UserId == Guid.Parse(userId));
            var result = _mapper.Map<QuizResultModel>(quizresult);
            return result;
        }

        public async Task<QuizViewModel> ListQuiz(string courseid)
        {

            var quizzlist = await _unitOfWork.GetRepository<Quiz>().GetByPropertyAsync(q => q.CourseId == courseid,includeProperties: "Questions,Questions.Answers");
            var result = _mapper.Map<QuizViewModel>(quizzlist);
            return result;
        }

        public async Task TakeQuiz(QuizzSubmissionModel quizzSubmissionModel)
        {
            var userId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);

            int score = 0;
            foreach (var item in quizzSubmissionModel.SubmittedAnswers)
            {
                var selected  = await _unitOfWork.GetRepository<QuizAnswer>().GetByPropertyAsync(a=>a.QuestionId == item.QuestionId
                && a.Id == item.AnswerSellectedId);

                if(selected != null && selected.IsCorrect == true) score++;
            }
            var quizresult = new QuizResult()
            {
                UserId = Guid.Parse(userId),
                QuizId = quizzSubmissionModel.QuizId,
                Score = score
            };
            await _unitOfWork.GetRepository<QuizResult>().AddAsync(quizresult);
            await _unitOfWork.SaveAsync();

        }
    }
}
