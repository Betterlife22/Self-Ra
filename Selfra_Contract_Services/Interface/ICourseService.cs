using Selfra_Core.Base;
using Selfra_ModelViews.Model.CourseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Contract_Services.Interface
{
    public interface ICourseService
    {
        public Task CreateCourse(CourseModifyModel courseModifyModel);
        public Task UpdateCourse(string courseid,CourseModifyModel courseModifyModel);
        public Task<PaginatedList<CourseViewModel>> GetAllCourse(int index, int pageSize);
        public Task<CourseViewModel> GetCourseById(string id);
    }
}
