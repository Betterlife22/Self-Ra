using AutoMapper;
using Selfra_Contract_Services.Interface;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.QuizzModel;
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
        public QuizzService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Createquiz(QuizzModifyModel quizzModifyModel)
        {
            var quiz = _mapper.Map<Quiz>(quizzModifyModel);
            await _unitOfWork.GetRepository<Quiz>().AddAsync(quiz);
            foreach (var item in quizzModifyModel.Questions)
            {
                var question = _mapper.Map<QuizQuestion>(item);
                await _unitOfWork.GetRepository<QuizQuestion>().AddAsync(question);
                foreach (var answer in item.Answers)
                {
                    var ans = _mapper.Map<QuizAnswer>(answer);
                    await _unitOfWork.GetRepository<QuizAnswer>().AddAsync(ans);
                }
            }
            await _unitOfWork.SaveAsync();

        }

        public async Task TakeQuiz(QuizzSubmissionModel quizzSubmissionModel)
        {
            int score = 0;
            foreach (var item in quizzSubmissionModel.SubmittedAnswers)
            {
                var selected  = await _unitOfWork.GetRepository<QuizAnswer>().GetByPropertyAsync(a=>a.QuestionId == item.QuestionId
                && a.Id == item.AnswerSellectedId);

                if(selected != null && selected.IsCorrect == true) score++;
            }
            var quizresult = new QuizResult()
            {
                UserId = Guid.Parse(quizzSubmissionModel.UserId),
                QuizId = quizzSubmissionModel.QuizId,
                Score = score
            };
            await _unitOfWork.GetRepository<QuizResult>().AddAsync(quizresult);
            await _unitOfWork.SaveAsync();

        }
    }
}
