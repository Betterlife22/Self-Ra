using AutoMapper;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.BookModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Services.MapperProfile
{
    public class BookMapperProfile :Profile
    {
        public BookMapperProfile()
        {
            CreateMap<Book,BookViewModel>().ReverseMap();
            CreateMap<Book,BookModifyModel>().ReverseMap();
            CreateMap<ReadingProgress,BookProgessModel>().ReverseMap();
            CreateMap<ReadingProgress, BookProgressViewModel>().ReverseMap();
        }
    }
}
