using Selfra_ModelViews.Model.LessonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Contract_Services.Interface
{
    public interface ILessonService
    {
        Task<LessonViewModel> GetLessonById(string lessonid);
        Task<List<LessonViewModel>> GetAllLessonInCourse(string courseid);
        Task CreateLesson(LessonModifyModel lessonModifyModel);
    }
}
