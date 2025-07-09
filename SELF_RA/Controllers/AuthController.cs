
using Microsoft.AspNetCore.Mvc;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_ModelViews.Model.UserModel;

namespace SELF_RA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IFireBaseService _firebaseService;
        public AuthController(IAuthService authService, IFireBaseService firebaseService)
        {
            _authService = authService;
            _firebaseService = firebaseService;
        }

        [HttpGet]
        [Route("me")]
        public async Task<IActionResult> GetUserInfo()
        {
            UserInfoModel model = await _authService.GetUserInfo();
            return Ok(BaseResponseModel<string>.OkDataResponse(model, "Lấy thông tin thành công"));
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            TokenResponse model = await _authService.Login(loginRequest);
            await _firebaseService.SaveOrUpdateTokenAsync(loginRequest.UserName, loginRequest.FcmToken);


            return Ok(BaseResponseModel<string>.OkDataResponse(model, "Đăng nhập thành công"));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register (RegisterRequestModel registerRequest)
        {
            await _authService.Register(registerRequest);
            return Ok(BaseResponseModel<string>.OkMessageResponseModel("Đăng ký tài khoản thành công"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _authService.Delete(id);
            return Ok(BaseResponseModel<string>.OkMessageResponseModel("Xóa tài khoản thành công"));
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestModel refreshTokenRequest)
        {
            TokenResponse model = await _authService.RefreshToken(refreshTokenRequest);
            return Ok(BaseResponseModel<TokenResponse>.OkDataResponse(model, "Tạo mới token thành công"));
        }
    }
}
