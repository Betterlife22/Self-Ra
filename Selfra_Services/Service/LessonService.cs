using AutoMapper;
using Selfra_Contract_Services.Interface;
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

        public async Task<List<LessonViewModel>> GetAllLessonInCourse(string courseid)
        {
            var LessonList = await _unitOfWork.GetRepository<Lesson>().GetAllByPropertyAsync(l=>l.CourseId == courseid);
            var result = _mapper.Map<List<LessonViewModel>>(LessonList);
            return result;
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
