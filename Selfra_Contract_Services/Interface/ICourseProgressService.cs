using Selfra_ModelViews.Model.ProgressModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Contract_Services.Interface
{
    public interface ICourseProgressService
    {
        Task<CourseProgessViewModel> GetUserCourseProgessAsync(string userid, string courseid);
        Task<List<CourseProgessViewModel>> GetAllUserCourseProgessAsync(string userid);
        Task EnrollCourse(CourseEnrollModel courseEnrollModel);

        //Task<List<LessonProgressViewModel>> GetLessonProgressInCourse(string courseid, string userid);
        Task StartLesson(LessonStartModel lessonStartModel);
        Task CalculateProgress(string userid, string courseid);

        Task MarkLessonComplete(string userid, string lessonid);



    }
}
