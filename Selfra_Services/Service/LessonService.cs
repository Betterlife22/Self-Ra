using AutoMapper;
using AutoMapper.QueryableExtensions;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.CourseModel;
using Selfra_ModelViews.Model.LessonModel;
using Selft.Contract.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Services.Service
{
    public class LessonService : ILessonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LessonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task CreateLesson(LessonModifyModel lessonModifyModel)
        {
            string videourl = await _unitOfWork.UploadFileAsync(lessonModifyModel.VideoUrl);
            var lesson = _mapper.Map<Lesson>(lessonModifyModel);
            lesson.VideoUrl = videourl;
            await _unitOfWork.GetRepository<Lesson>().AddAsync(lesson);
            await _unitOfWork.SaveAsync();
        }

        public async Task<PaginatedList<LessonViewModel>> GetAllLessonInCourse(string courseid, int index, int pageSizes)
        {
            var LessonList = _unitOfWork.GetRepository<Lesson>().GetQueryableByProperty(l=>l.CourseId == courseid);
            var Querycourse = LessonList.ProjectTo<LessonViewModel>(_mapper.ConfigurationProvider);
            PaginatedList<LessonViewModel> paginateList = await _unitOfWork.GetRepository<LessonViewModel>().GetPagingAsync(Querycourse.AsQueryable(), index, pageSizes); 
            return paginateList;
        }

        public async Task<LessonViewModel> GetLessonById(string lessonid)
        {
            var Lesson = await _unitOfWork.GetRepository<Lesson>().GetByIdAsync(lessonid);
            if(Lesson == null)
            {
                return null;
            }
            var result = _mapper.Map<LessonViewModel>(Lesson);
            return result;
        }
    }
}
