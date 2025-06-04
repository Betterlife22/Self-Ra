using Selfra_Core.Base;
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
        Task<PaginatedList<LessonViewModel>> GetAllLessonInCourse(string courseid, int index, int pageSizes);
        Task CreateLesson(LessonModifyModel lessonModifyModel);
    }
}
