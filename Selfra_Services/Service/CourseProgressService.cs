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

        public async Task CalculateProgress(string userid, string courseid)
        {
            //get total lesson in course
            var lesson = await _unitOfWork.GetRepository<Lesson>().GetAllByPropertyAsync(l=>l.CourseId == courseid);
            int lessoncount = lesson.Count();
            if (lessoncount == 0) return;

            //get completed lesson 
            var completedLesson = await _unitOfWork.GetRepository<UserLessonProgress>().GetAllByPropertyAsync(
                ul => ul.UserId == Guid.Parse(userid)
                && ul.IsCompleted
                && ul.Lesson != null
                && ul.Lesson.CourseId == courseid,
                includeProperties: "Lesson");

            var completedcount = completedLesson.Count();
            float progressPercentage = (float)completedcount / lessoncount * 100;

            var usercourseProgress = await _unitOfWork.GetRepository<UserCourseProgress>().GetByPropertyAsync(
                uc => uc.UserId == Guid.Parse(userid)
                && uc.CourseId == courseid
                );
            if(usercourseProgress == null) return;
            usercourseProgress.ProgressPercentage = progressPercentage;
            if(progressPercentage == 100)
            {
                usercourseProgress.IsCompleted = true;
                usercourseProgress.CompletedAt = DateTime.Now;
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task EnrollCourse(CourseEnrollModel courseEnrollModel)
        {
            var courseProgress = _mapper.Map<UserCourseProgress>(courseEnrollModel);
            courseProgress.UserId = Guid.Parse(courseEnrollModel.UserId);
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

        public async Task MarkLessonComplete(string userid, string lessonid)
        {
            var userlessonProgress = new UserLessonProgress()
            {
                UserId = Guid.Parse(userid),
                LessonId = lessonid,
                IsCompleted = true,
                LastUpdatedTime = DateTime.UtcNow

            };
            await _unitOfWork.GetRepository<UserLessonProgress>().AddAsync(userlessonProgress);
            await _unitOfWork.SaveAsync();
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
