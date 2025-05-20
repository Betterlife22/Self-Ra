using AutoMapper;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.CategoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Services.MapperProfile
{
    public class CategoryMapper :Profile 
    {
        public CategoryMapper()
        {
            CreateMap<Category, CategoryViewModel>(). ForMember(dest => dest.CeatorName,
               opt => opt.MapFrom(src => src.Creator != null ? src.Creator.UserName : "Unknown"))
               .ReverseMap();

            CreateMap<Category, CategoryCreateModel>().ReverseMap();
        }
    }
}
