using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_ModelViews.Model.QuizzModel;

namespace SELF_RA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzController : ControllerBase
    {
        private readonly IQuizzService _quizzService;
        public QuizzController(IQuizzService quizzService)
        {
            _quizzService = quizzService;
        }
        [HttpPost("CreateQuizz")]
        public async Task<IActionResult> CreateQuizz([FromBody] QuizzModifyModel quizzModifyModel)
        {
            await _quizzService.Createquiz(quizzModifyModel);
            var response = BaseResponseModel<string>.OkMessageResponseModel("Create successfull");
            return new OkObjectResult(response);
        }
        [HttpPost("TakeQuiz")]
        public async Task<IActionResult> TakeQuiz([FromBody] QuizzSubmissionModel quizzSubmissionModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _quizzService.TakeQuiz(quizzSubmissionModel);
            var response = BaseResponseModel<string>.OkMessageResponseModel("Take quiz successfull");
            return new OkObjectResult(response);
        }
        [HttpGet("GetQuiz")]
        public async Task<IActionResult> GetQuiz([FromQuery]string courseid)
        {
            var quizlist = await _quizzService.ListQuiz(courseid);
            var response = BaseResponseModel<QuizViewModel>.OkDataResponse(quizlist, "Load successfull");
            return new OkObjectResult(response);
        }
        [HttpGet("GetUserQuizResult")]
        public async Task<IActionResult> GetUserQuizResult([FromQuery] string quizid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userresult = await _quizzService.GetUserQuizResult(quizid);
            var response = BaseResponseModel<QuizResultModel>.OkDataResponse(userresult, "Load successfull");
            return new OkObjectResult(response);
        }
    }
}
