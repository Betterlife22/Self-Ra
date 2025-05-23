using AutoMapper;
using Selfra_Contract_Services.Interface;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.CourseModel;
using Selft.Contract.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<List<CourseViewModel>> GetAllCourse()
        {
            var courselist = await _unitOfWork.GetRepository<Course>().GetAllAsync();
            var result = _mapper.Map<List<CourseViewModel>>(courselist);
            return result;
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
