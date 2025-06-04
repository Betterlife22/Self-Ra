using AutoMapper;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_Entity.Model;
using AutoMapper.QueryableExtensions;

using Selfra_ModelViews.Model.CourseModel;
using Selfra_ModelViews.Model.RoleModel;
using Selft.Contract.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Selfra_Services.Service
{
    internal class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task CreateCourse(CourseModifyModel courseModifyModel)
        {
            string thumbnailUrl = await _unitOfWork.UploadFileAsync(courseModifyModel.ThumbnailUrl);
            var course = _mapper.Map<Course>(courseModifyModel);
            course.ThumbnailUrl = thumbnailUrl;
            await _unitOfWork.GetRepository<Course>().AddAsync(course);
            await _unitOfWork.SaveAsync();
        }

        public async Task<PaginatedList<CourseViewModel>> GetAllCourse(int index, int pageSize)
        {

            var courselist = _unitOfWork.GetRepository<Course>().GetQueryableByProperty();
            var Querycourse = courselist.ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider);
            PaginatedList<CourseViewModel> paginatedourse = await _unitOfWork.GetRepository<CourseViewModel>().GetPagingAsync(Querycourse.AsQueryable(), index, pageSize);

            return paginatedourse;
        }

        public async Task<CourseViewModel> GetCourseById(string id)
        {
            var course = await _unitOfWork.GetRepository<Course>().GetByIdAsync(id);
            if (course == null)
            {
                return null;
            }
            var result = _mapper.Map<CourseViewModel>(course);
            return result;
        }

        public Task UpdateCourse(string courseid, CourseModifyModel courseModifyModel)
        {
            throw new NotImplementedException();
        }
    }
}
