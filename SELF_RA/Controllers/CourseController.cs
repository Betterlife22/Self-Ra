using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.CourseModel;
using Selfra_ModelViews.Model.ProgressModel;

namespace SELF_RA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ICourseProgressService _courseProgressService;
        public CourseController(ICourseService courseService, ICourseProgressService courseProgressService)
        {
            _courseService = courseService;
            _courseProgressService = courseProgressService;
        }
        [HttpPost("CreateCourse")]
        public async Task<IActionResult> CreateCourse([FromForm] CourseModifyModel courseModifyModel)
        {
            await _courseService.CreateCourse(courseModifyModel);
            var response = BaseResponseModel<string>.OkMessageResponseModel("Create Successfull");
            return new OkObjectResult(response);
        }
        [HttpGet("GetAllCourse")]
        public async Task<IActionResult> GetAllCourse()
        {
            var courseList = await _courseService.GetAllCourse();
            var response = BaseResponseModel<List<CourseViewModel>>.OkDataResponse(courseList, "Load successfull");
            return new OkObjectResult(response);
        }
        [HttpGet("GetCourseById")]
        public async Task<IActionResult> GetCourseById([FromQuery] string id)
        {
            var course = await _courseService.GetCourseById(id);
            if(course == null)
            {
                return new NotFoundObjectResult("Can not find the course");
            }
            var response = BaseResponseModel<CourseViewModel>.OkDataResponse(course, "Load successfully");
            return new OkObjectResult(response);
        }

        [HttpPost("EnrollCourse")]
        public async Task<IActionResult> EnrollCourse([FromBody] CourseEnrollModel courseEnrollModel , [FromQuery] string userid)
        {
            await _courseProgressService.EnrollCourse(courseEnrollModel , userid);
            var response = BaseResponseModel<string>.OkMessageResponseModel("Enroll Successfull");
            return new OkObjectResult(response);
        }
        [HttpGet("GetUserCourseProgress")]
        public async Task<IActionResult> GetUserCourseProgress([FromQuery] string userid, string courseid)
        {
            var courseprogress = await _courseProgressService.GetUserCourseProgessAsync(userid, courseid);
            var response = BaseResponseModel<CourseProgessViewModel>.OkDataResponse(courseprogress, "Load successfully");
            return new OkObjectResult(response);
        }
        [HttpGet("GetAllUserCourseProgress")]
        public async Task<IActionResult> GetAllUserCourseProgress([FromQuery] string userid)
        {
            var courseprogressList = await _courseProgressService.GetAllUserCourseProgessAsync(userid);
            var response = BaseResponseModel<List<CourseProgessViewModel>>.OkDataResponse(courseprogressList, "Load successfully");
            return new OkObjectResult(response);
        }
    }
}
