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
        public async Task<IActionResult> CreateQuizz([FromForm] QuizzModifyModel quizzModifyModel)
        {
            await _quizzService.Createquiz(quizzModifyModel);
            var response = BaseResponseModel<string>.OkMessageResponseModel("Create successfull");
            return new OkObjectResult(response);
        }
        [HttpPost("TakeQuiz")]
        public async Task<IActionResult> TakeQuiz([FromBody] QuizzSubmissionModel quizzSubmissionModel)
        {
            await _quizzService.TakeQuiz(quizzSubmissionModel);
            var response = BaseResponseModel<string>.OkMessageResponseModel("Take quiz successfull");
            return new OkObjectResult(response);
        }
    }
}
