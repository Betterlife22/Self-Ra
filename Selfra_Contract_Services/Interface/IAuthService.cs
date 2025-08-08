
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.UserModel;

namespace Selfra_Contract_Services.Interface
{
    public interface IAuthService
    {
        Task<TokenResponse> Login (LoginRequest loginRequest);
        Task Register(RegisterRequestModel model);

        Task<UserInfoModel> GetUserInfo();

        Task<ApplicationUser> GetUserById(Guid? id);
        Task Delete(string id);

        Task<TokenResponse> RefreshToken(RefreshTokenRequestModel request);

    }
}
