using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_ModelViews.Model.LessonModel;

namespace SELF_RA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpPost("CreateLesson")]
        public async Task<IActionResult> CreateLesson([FromForm] LessonModifyModel lessonModifyModel)
        {
            await _lessonService.CreateLesson(lessonModifyModel);
            var response = BaseResponseModel<string>.OkMessageResponseModel("Create successfully");
            return new OkObjectResult(response);
        }
        [HttpGet("GetLessonById")]
        public async Task <IActionResult> GetLessonById([FromQuery] string lessonId)
        {
            var lesson = await _lessonService.GetLessonById(lessonId);
            if(lesson == null)
            {
                return new NotFoundObjectResult("Do not find a lesson");
            }
            var response = BaseResponseModel<LessonViewModel>.OkDataResponse(lesson,"Load successfully");
            return new OkObjectResult(response);
        }
        [HttpGet("GetAllLessonInCourse")]
        public async Task<IActionResult> GetAllLessonInCourse (string courseid)
        {
            var lessonlist = await _lessonService.GetAllLessonInCourse(courseid);
            var response = BaseResponseModel<List<LessonViewModel>>.OkDataResponse(lessonlist, "Load successfully");
            return new OkObjectResult(response);
        }
    }
}
