using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Selfra_Contract_Services.Interface;
using Selfra_ModelViews.Model.CourseModel;
using Selfra_ModelViews.Model.LessonModel;
using Selfra_ModelViews.Model.MentorContact;
using Selfra_ModelViews.Model.MentorModel;
using Selfra_ModelViews.Model.PackageModel;
using Selfra_ModelViews.Model.UserModel;

namespace SELF_RA.Controllers
{
    [Route("odata")]
    //[ApiController]
    public class ODataManageController : ODataController
    {
        private readonly IODataService _oDataService;
        public ODataManageController(IODataService oDataService)
        {
            _oDataService = oDataService;
        }
        [HttpGet("Courses")]

        [EnableQuery]
        public IQueryable<CourseViewModel> GetCourses()
        {
            return _oDataService.GetCourses();
        }
        [HttpGet("Lessons")]
        [EnableQuery]
        public IQueryable<LessonViewModel> GetLessons()
        {
            return _oDataService.GetLessons();
        }
        [HttpGet("MentorContacts")]
        [EnableQuery]
        public IQueryable <ResponseMentorContact> GetMentorContacts()
        {
            return _oDataService.GetMentorContacts();
        }
        [HttpGet("Packages")]
        [EnableQuery]
        public IQueryable<ResponsePackageModel> GetPackageModels()
        {
            return _oDataService.GetPackages();
        }
        [HttpGet("Mentors")]
        [EnableQuery]
        public IQueryable<ResponseMentorModel> GetMentorModels()
        {
            return _oDataService.GetMentors();
        }
        [HttpGet("ApplicationUsers")]
        [EnableQuery]
        public IQueryable<ResponseUserModel> GetUsers()
        {
            return _oDataService.GetUsers();
        }
    }
}
