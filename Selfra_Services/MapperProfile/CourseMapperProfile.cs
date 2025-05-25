using AutoMapper;
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
            CreateMap<Course, CourseViewModel>().ReverseMap();
            CreateMap<UserCourseProgress, CourseProgessViewModel>().ReverseMap();
            CreateMap<UserCourseProgress, CourseEnrollModel>().ReverseMap();

            CreateMap<Category,CategoryViewModel>().ReverseMap();
            CreateMap<Category,CategoryModifyModel>().ReverseMap();

            CreateMap<Quiz,QuizzModifyModel>().ReverseMap();
            CreateMap<QuizQuestion,QuestionModifyModel>().ReverseMap();
            CreateMap<QuizAnswer,AnswerModifyModel>().ReverseMap();
        }
    }
}
