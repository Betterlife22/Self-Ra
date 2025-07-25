﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.CategoryModel;
using Selfra_ModelViews.Model.CourseModel;
using Selfra_ModelViews.Model.ProgressModel;
using Selfra_Services.Infrastructure;
using System.Security.Claims;

namespace SELF_RA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ICourseProgressService _courseProgressService;
        private readonly ICategoryService _categoryService;

        public CourseController(ICourseService courseService, ICourseProgressService courseProgressService, ICategoryService categoryService)
        {
            _courseService = courseService;
            _courseProgressService = courseProgressService;
            _categoryService = categoryService;
        }
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromForm] CategoryModifyModel categoryModifyModel)
        {
            await _categoryService.CreateCategory(categoryModifyModel);
            var response = BaseResponseModel<string>.OkMessageResponseModel("Create Successfull");
            return new OkObjectResult(response);
        }
        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            var categories = await _categoryService.GetAllCategory();
            return new OkObjectResult(categories);
        }
        [HttpPost("CreateCourse")]
        public async Task<IActionResult> CreateCourse([FromForm] CourseModifyModel courseModifyModel)
        {
            await _courseService.CreateCourse(courseModifyModel);
            var response = BaseResponseModel<string>.OkMessageResponseModel("Create Successfull");
            return new OkObjectResult(response);
        }
        [HttpGet("GetAllCourse")]
        public async Task<IActionResult> GetAllCourse(int index, int pageSize)
        {
            var courseList = await _courseService.GetAllCourse(index,pageSize);
            var response = BaseResponseModel<PaginatedList<CourseViewModel>>.OkDataResponse(courseList, "Load successfull");
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
        [HttpGet("GetCoursesByCategory")]
        public async Task<IActionResult> GetCoursesByCategory([FromQuery] string CategoryId)
        {
            var courselist = await _categoryService.GetCategoryById(CategoryId);
            var response = BaseResponseModel<CategoryViewModel>.OkDataResponse(courselist, "Load successfully");
            return new OkObjectResult(response);

        }
        [HttpPost("EnrollCourse")]
        public async Task<IActionResult> EnrollCourse([FromBody] CourseEnrollModel courseEnrollModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _courseProgressService.EnrollCourse(courseEnrollModel);
                var response = BaseResponseModel<string>.OkMessageResponseModel("Enroll Successfull");
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message.ToString());
            }
            
        }
        [HttpGet("GetUserCourseProgress")]
        public async Task<IActionResult> GetUserCourseProgress([FromQuery]  string courseid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var courseprogress = await _courseProgressService.GetUserCourseProgessAsync(courseid);
            var response = BaseResponseModel<CourseProgessViewModel>.OkDataResponse(courseprogress, "Load successfully");
            return new OkObjectResult(response);
        }
        [HttpGet("GetAllUserCourseProgress")]
        public async Task<IActionResult> GetAllUserCourseProgress(int index, int pageSize)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var email = User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;

            var courseprogressList = await _courseProgressService.GetAllUserCourseProgessAsync(index,pageSize);
            var response = BaseResponseModel<List<CourseProgessViewModel>>.OkDataResponse(courseprogressList, "Load successfully");
            return new OkObjectResult(response);
        }
        //[HttpPost("StartLesson")]
        //public async Task <IActionResult> StartLesson([FromBody] LessonStartModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    await _courseProgressService.StartLesson(model);
        //    var response = BaseResponseModel<string>.OkMessageResponseModel("Start successfully");
        //    return new OkObjectResult(response);
        //}

        [HttpPut("MarkLessonCompleted")]
        public async Task<IActionResult> MarkLessonCompleted([FromQuery]string lessonid, string courseid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _courseProgressService.MarkLessonComplete(lessonid,courseid);
            var response = BaseResponseModel<string>.OkMessageResponseModel("update successfully");
            return new OkObjectResult(response);
        }
        //[HttpPut("UpdateProgress")]
        //public async Task<IActionResult> UpdateProgress([FromQuery] string courseid)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    await _courseProgressService.CalculateProgress(courseid);
        //    var response = BaseResponseModel<string>.OkMessageResponseModel("update successfully");
        //    return new OkObjectResult(response);
        //}
       

    }
}
