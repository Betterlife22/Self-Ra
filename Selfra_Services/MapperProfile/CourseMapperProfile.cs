using AutoMapper;
using Selfra_Core.Constaint;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.CategoryModel;
using Selfra_ModelViews.Model.CourseModel;
using Selfra_ModelViews.Model.ProgressModel;
using Selfra_ModelViews.Model.QuizzModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Services.MapperProfile
{
    public class CourseMapperProfile : Profile
    {
        public CourseMapperProfile()
        {
            CreateMap<Course, CourseModifyModel>().ReverseMap();
            CreateMap<Course, CourseViewModel>().ForMember(dest => dest.CreatorName,
               opt => opt.MapFrom(src => src.Creator != null ? src.Creator.UserName : "Unknown"))
               .ReverseMap();

            CreateMap<UserCourseProgress, CourseProgessViewModel>().ForMember(dest => dest.CourseName,
               opt => opt.MapFrom(src => src.Course != null ? src.Course.Title : "Unknown"))
            .ReverseMap();

            CreateMap<UserCourseProgress, CourseEnrollModel>().ReverseMap();

            CreateMap<Category,CategoryViewModel>().ForMember(dest => dest.CreatorName,
               opt => opt.MapFrom(src => src.Creator != null ? src.Creator.UserName : "Unknown")).ReverseMap();
            CreateMap<Category,CategoryModifyModel>().ReverseMap();

            CreateMap<Quiz,QuizzModifyModel>().ReverseMap();
            CreateMap<QuizQuestion,QuestionModifyModel>().ReverseMap();
            CreateMap<QuizAnswer,AnswerModifyModel>().ReverseMap();
        }
    }
}
