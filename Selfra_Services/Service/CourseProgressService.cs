using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.CourseModel;
using Selfra_ModelViews.Model.ProgressModel;
using Selfra_Services.Infrastructure;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CourseProgressService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CalculateProgress(string courseid, string userId)
        {
            //var userId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);

            //get total lesson in course
            var lesson = await _unitOfWork.GetRepository<Lesson>().GetAllByPropertyAsync(l=>l.CourseId == courseid);
            int lessoncount = lesson.Count();
            if (lessoncount == 0) return;

            //get completed lesson 
            var completedLesson = await _unitOfWork.GetRepository<UserLessonProgress>().GetAllByPropertyAsync(
                ul => ul.UserId == Guid.Parse(userId)
                && ul.IsCompleted
                && ul.Lesson != null
                && ul.Lesson.CourseId == courseid,
                includeProperties: "Lesson");

            var completedcount = completedLesson.Count();
            float progressPercentage = (float)completedcount / lessoncount * 100;

            var usercourseProgress = await _unitOfWork.GetRepository<UserCourseProgress>().GetByPropertyAsync(
                uc => uc.UserId == Guid.Parse(userId)
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
            var userId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);

            var courseProgress = _mapper.Map<UserCourseProgress>(courseEnrollModel);
            courseProgress.UserId = Guid.Parse(userId);
            await _unitOfWork.GetRepository<UserCourseProgress>().AddAsync(courseProgress);

            var course = await _unitOfWork.GetRepository<Course>().GetByPropertyAsync(uc => uc.Id == courseEnrollModel.CourseId, includeProperties: "Lessons");
            var lessonincourse = course.Lessons;
            foreach (var item in lessonincourse)
            {
                var lessonprogress = new UserLessonProgress()
                {
                    LessonId = item.Id,
                    UserId =Guid.Parse(userId),
                    IsCompleted = false,
                    CreatedTime = DateTime.Now,
                };
                await _unitOfWork.GetRepository<UserLessonProgress>().AddAsync(lessonprogress);
                await _unitOfWork.SaveAsync();

            }

            await _unitOfWork.SaveAsync();

        }

        public async Task<PaginatedList<CourseProgessViewModel>> GetAllUserCourseProgessAsync(int index, int pageSize)
        {
            var userId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);

            var courseList =  _unitOfWork.GetRepository<UserCourseProgress>().GetQueryableByProperty(uc => uc.UserId.ToString() == userId, includeProperties:"Course");
            var lessonlist =  _unitOfWork.GetRepository<UserLessonProgress>().GetQueryableByProperty(uc => uc.UserId.ToString() == userId, includeProperties: "Lesson");
            var courseViewModels = courseList.Select(c => new CourseProgessViewModel
            {
                CourseName = c.Course.Title ?? "Unknown",
                ProgressPercentage = c.ProgressPercentage,
                IsCompleted = c.IsCompleted,
                CompletedAt = c.CompletedAt,
            //    Lessons = lessonlist

            //    .Where(l => l.Lesson != null && l.Lesson.CourseId == c.CourseId)
            //    .Select(l => new LessonProgressViewModel
            //    {
            //        LessonName = l.Lesson.Title ?? "Unknown",
            //        IsCompleted = l.IsCompleted
            //    })
            //.ToList()
            });
            // Querycourse = courseViewModels.ProjectTo<CourseProgessViewModel>(_mapper.ConfigurationProvider);
            PaginatedList<CourseProgessViewModel> paginatedourse = await _unitOfWork.GetRepository<CourseProgessViewModel>().GetPagingAsync(courseViewModels.AsQueryable(), index, pageSize);
            //var result = _mapper.Map<List<CourseProgessViewModel>>(courseList);
            return paginatedourse;

        }

        public async Task<PaginatedList<LessonProgressViewModel>> GetAllUserUncompletedLesson(int index, int pageSize, string courseid)
        {
            var userId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);

            var course = await _unitOfWork.GetRepository<UserCourseProgress>().GetByPropertyAsync(uc => uc.UserId.ToString() == userId
            && uc.CourseId == courseid);
            var lessonList = await _unitOfWork.GetRepository<UserLessonProgress>().GetAllByPropertyAsync(ulp => ulp.UserId.ToString() == userId
            && ulp.Lesson != null && ulp.Lesson.CourseId == courseid);
            var result = lessonList.Select(l => new LessonProgressViewModel
            {
                LessonName = l.Lesson?.Title ?? "Unknown",
                IsCompleted = false
            }).ToList();
            PaginatedList<LessonProgressViewModel> paginatedlessons = await _unitOfWork.GetRepository<LessonProgressViewModel>()
                .GetPagingAsync(result.AsQueryable(), index, pageSize);
            return paginatedlessons;

        }

        public async Task<CourseProgessViewModel> GetUserCourseProgessAsync(string courseid)
        {
            var userId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);

            var course = await _unitOfWork.GetRepository<UserCourseProgress>().GetByPropertyAsync(uc => uc.UserId.ToString() == userId
            && uc.CourseId == courseid);
            var lessonList = await _unitOfWork.GetRepository<UserLessonProgress>().GetAllByPropertyAsync(ulp => ulp.UserId.ToString() == userId
            && ulp.Lesson != null && ulp.Lesson.CourseId == courseid);
            var courseViewModel = new CourseProgessViewModel
            {
                CourseName = course?.Course?.Title ?? "Unknown",
                ProgressPercentage = course?.ProgressPercentage ?? 0,
                IsCompleted = course?.IsCompleted ?? false,
                CompletedAt = course?.CompletedAt,
                Lessons = lessonList.OrderBy(l => l.Lesson != null ? l.Lesson.OrderIndex : int.MaxValue)
                .Select(l => new LessonProgressViewModel
                {
                    LessonId = l.LessonId,
                    LessonName = l.Lesson?.Title ?? "Unknown",
                    OrderIndex = l.Lesson?.OrderIndex?? 0,
                    IsCompleted = l.IsCompleted
                }).ToList()
            };
            //var result = _mapper.Map<CourseProgessViewModel>(course);
            return courseViewModel;
        }

        public async Task MarkLessonComplete(string lessonid, string courseid)
        {
            var userId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);
            var lessonexisted = await _unitOfWork.GetRepository<Lesson>().GetByPropertyAsync(l => l.Id == lessonid);            
            var userlessonProgress = await _unitOfWork.GetRepository<UserLessonProgress>()
                .GetByPropertyAsync(l=>l.LessonId == lessonid && l.UserId == Guid.Parse(userId));
            userlessonProgress.IsCompleted = true;
            userlessonProgress.LastUpdatedTime = DateTime.UtcNow;
            await _unitOfWork.SaveAsync();
            await CalculateProgress(courseid,userId);
        }

        public async Task StartLesson(LessonStartModel lessonStartModel)
        {
            var userId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);

            var lesson = _mapper.Map<UserLessonProgress>(lessonStartModel);
            lesson.UserId = Guid.Parse(userId);
            await _unitOfWork.GetRepository<UserLessonProgress>().AddAsync(lesson);
            await _unitOfWork.SaveAsync();
        }
    }
}
