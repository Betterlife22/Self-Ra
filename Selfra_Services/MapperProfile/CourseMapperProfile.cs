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
                .ForMember(dest => dest.Lessons, opt => opt.MapFrom(src =>
                src.Lessons.OrderBy(l => l.OrderIndex)))
               .ReverseMap();
           
            CreateMap<UserCourseProgress, CourseProgessViewModel>().ForMember(dest => dest.CourseName,
               opt => opt.MapFrom(src => src.Course != null ? src.Course.Title : "Unknown"))
            .ReverseMap();

            CreateMap<UserCourseProgress, CourseEnrollModel>().ReverseMap();

            CreateMap<Category,CategoryViewModel>().ForMember(dest => dest.CreatorName,
               opt => opt.MapFrom(src => src.Creator != null ? src.Creator.UserName : "Unknown")).ReverseMap();
            CreateMap<Category,CategoryModifyModel>().ReverseMap();

            CreateMap<Quiz,QuizViewModel>().ReverseMap();
            CreateMap<QuizQuestion,QuestionViewModel>().ReverseMap();
            CreateMap<QuizAnswer,AnswerViewModel>().ReverseMap();

            CreateMap<Quiz, QuizzModifyModel>().ReverseMap();
            CreateMap<QuizQuestion, QuestionModifyModel>()
                  .ReverseMap()
                  .ForMember(dest => dest.Quiz, opt => opt.Ignore())            
                  .ForMember(dest => dest.Answers, opt => opt.Ignore());
            CreateMap<AnswerModifyModel, QuizAnswer>()
                .ForMember(dest => dest.Question, opt => opt.Ignore());
            CreateMap<QuizAnswer, AnswerModifyModel>()
                .ReverseMap()
                .ForMember(dest => dest.Question, opt => opt.Ignore());
            CreateMap<QuizResult, QuizResultModel>().ForMember(dest => dest.QuizName,
               opt => opt.MapFrom(src => src.Quiz != null ? src.Quiz.Title : "Unknown")).
               ForMember(dest => dest.UserName,
               opt => opt.MapFrom(src => src.User != null ? src.User.UserName : "Unknown"))
               .ReverseMap();

        }
    }
}
