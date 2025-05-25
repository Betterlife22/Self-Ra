

using AutoMapper;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.RoleModel;
using Selfra_ModelViews.Model.UserModel;

namespace Selfra_Services.MapperProfile
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<CreateRoleModel, ApplicationRole>().ReverseMap();
            CreateMap<UpdateRoleModel, ApplicationRole>().ReverseMap();
            CreateMap<ResponseRoleModel, ApplicationRole>().ReverseMap();


            CreateMap<RegisterRequestModel, ApplicationUser>().ReverseMap();
            CreateMap<ResponseUserModel, ApplicationUser>().ReverseMap();
        }
    }
}
