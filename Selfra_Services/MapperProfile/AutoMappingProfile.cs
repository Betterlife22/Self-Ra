

using AutoMapper;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.FoodDetailModel;
using Selfra_ModelViews.Model.MentorContact;
using Selfra_ModelViews.Model.MentorModel;
using Selfra_ModelViews.Model.PostModel;
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


            CreateMap<CreateFoodDetailModel, FoodDetail>().ReverseMap();
            CreateMap<UpdateFoodDetailModel, FoodDetail>().ReverseMap();
            CreateMap<ResponseFoodDetailModel, FoodDetail>().ReverseMap();

            CreateMap<CreatePostModel, Post>().ReverseMap();
            CreateMap<UpdatePostModel, Post>().ReverseMap();
            CreateMap<ResponsePostModel, Post>().ReverseMap();

            CreateMap<CreateMentorModel, Mentor>().ReverseMap();
            CreateMap<UpdateMentorModel, Mentor>().ReverseMap();
            CreateMap<ResponseMentorModel, Mentor>().ReverseMap();

            CreateMap<CreateMentorContact ,MentorContact>().ReverseMap();
            CreateMap<UpdateMentorContact, MentorContact>().ReverseMap();
            CreateMap<ResponseMentorContact, MentorContact>().ReverseMap();
        }
    }
}
