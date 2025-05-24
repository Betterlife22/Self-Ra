

using AutoMapper;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.RoleModel;

namespace Selfra_Services.MapperProfile
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<CreateRoleModel, ApplicationRole>().ReverseMap();
            CreateMap<UpdateRoleModel, ApplicationRole>().ReverseMap();
            CreateMap<ResponseRoleModel, ApplicationRole>().ReverseMap();
        }
    }
}
