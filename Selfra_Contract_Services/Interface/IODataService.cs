using Selfra_ModelViews.Model.CourseModel;
using Selfra_ModelViews.Model.LessonModel;
using Selfra_ModelViews.Model.MentorContact;
using Selfra_ModelViews.Model.MentorModel;
using Selfra_ModelViews.Model.PackageModel;
using Selfra_ModelViews.Model.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Contract_Services.Interface
{
    public interface IODataService
    {
        IQueryable<CourseViewModel> GetCourses();
        IQueryable<LessonViewModel> GetLessons();
        IQueryable<ResponsePackageModel> GetPackages();
        IQueryable<ResponseMentorContact> GetMentorContacts();
        IQueryable<ResponseMentorModel> GetMentors();
        IQueryable<ResponseUserModel> GetUsers();
    }
}
