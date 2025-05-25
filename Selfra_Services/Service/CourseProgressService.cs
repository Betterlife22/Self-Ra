using AutoMapper;
using Selfra_Contract_Services.Interface;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.ProgressModel;
using Selft.Contract.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Services.Service
{
    public class CourseProgressService : ICourseProgressService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CourseProgressService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task EnrollCourse(CourseEnrollModel courseEnrollModel, string userid)
        {
            var courseProgress = _mapper.Map<UserCourseProgress>(courseEnrollModel);
            courseProgress.UserId = Guid.Parse(userid);
            await _unitOfWork.GetRepository<UserCourseProgress>().AddAsync(courseProgress);
            await _unitOfWork.SaveAsync();

        }

        public async Task<List<CourseProgessViewModel>> GetAllUserCourseProgessAsync(string userid)
        {
            var courseList = await _unitOfWork.GetRepository<UserCourseProgress>().GetAllByPropertyAsync(uc => uc.UserId.ToString() == userid);
            var result = _mapper.Map<List<CourseProgessViewModel>>(courseList);
            return result;

        }

       

        public async Task<CourseProgessViewModel> GetUserCourseProgessAsync(string userid, string courseid)
        {
            var course = await _unitOfWork.GetRepository<UserCourseProgress>().GetByPropertyAsync(uc => uc.UserId.ToString() == userid
            && uc.CourseId == courseid);
            var result = _mapper.Map<CourseProgessViewModel>(course);
            return result;
        }
       

        public async Task StartLesson(LessonStartModel lessonStartModel, string userid)
        {
            var lesson = _mapper.Map<UserLessonProgress>(lessonStartModel);
            lesson.UserId = Guid.Parse(userid);
            await _unitOfWork.GetRepository<UserLessonProgress>().AddAsync(lesson);
            await _unitOfWork.SaveAsync();
        }
    }
}
