using AutoMapper;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.LessonModel;
using Selfra_ModelViews.Model.ProgressModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Services.MapperProfile
{
    public class LessonMapperProfile: Profile
    {
        public LessonMapperProfile()
        {
            CreateMap<Lesson, LessonViewModel>().ReverseMap();
            CreateMap<Lesson, LessonModifyModel>().ReverseMap();
            CreateMap<UserLessonProgress, LessonProgressViewModel>().ForMember(dest => dest.LessonName,
            opt => opt.MapFrom(src => src.Lesson != null ? src.Lesson.Title : "Unknown")).ReverseMap();
            CreateMap<UserLessonProgress, LessonStartModel>().ReverseMap();

        }
    }
}
