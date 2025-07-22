using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_ModelViews.Model.MentorContact;
using Selfra_ModelViews.Model.MentorModel;
using Selfra_ModelViews.Model.UserModel;

namespace SELF_RA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentorContactController : ControllerBase
    {
        private readonly IMentorContactService _mentorContactService;
        private readonly IAuthService _authService;
        private readonly IFireBaseService _firebaseSerivce;
        private readonly IMentorService _mentorService;
        public MentorContactController(IMentorContactService mentorContactService, IAuthService authService,IFireBaseService firebaseSerivce, IMentorService mentorService)
        {
            _mentorContactService = mentorContactService;
            _authService = authService;
            _firebaseSerivce = firebaseSerivce;
            _mentorService = mentorService;
        }

        [HttpPost("CreateMentorContact")]
        public async Task<IActionResult> CreateMentorContact(CreateMentorContact model)
        {
            
            await _mentorContactService.CreateMentorContact(model);
            var mentor = await _mentorService.GetMentorById(model.MentorId);

            var token = await _firebaseSerivce.GetTokenByUserIdAsync(mentor.UserId.ToString());
            
            if (!string.IsNullOrEmpty(token))
            {
                await _firebaseSerivce.SendNotificationAsync(token,
                    "New Contact Request",
                    $"User {model.UserId} says: {model.Message}");
            }
            return Ok(BaseResponse<string>.OkMessageResponseModel("Tạo mới MentorContact thành công"));
        }

        [HttpGet("GetAllMentorContact")]
        public async Task<IActionResult> GetAllMentorContact(string? MentorId, int index = 1, int PageSize = 10)
        {
            PaginatedList<ResponseMentorContact> list = await _mentorContactService.GetAllMentorContact(MentorId, index, PageSize);
            return Ok(BaseResponse<ResponseMentorContact>.OkDataResponse(list, "Lấy danh sách thành công"));
        }
        [HttpGet("GetMentorContactById")]
        public async Task<IActionResult> GetMentorContactById(string id)
        {
            ResponseMentorContact model = await _mentorContactService.GetMentorContactById(id);
            return Ok(BaseResponse<ResponseMentorContact>.OkDataResponse(model, "Lấy MentorContact thành công"));
        }

        [HttpPut("UpdateMentorContact")]
        public async Task<IActionResult> UpdateMentorContact(UpdateMentorContact model)
        {
            await _mentorContactService.UpdateMentorContact(model);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Cập nhật MentorContact thành công"));
        }

        [HttpDelete("DeleteMentorContact")]
        public async Task<IActionResult> DeleteMentorContact(string id)
        {
            await _mentorContactService.DeleteMentorContact(id);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Xóa MentorContact thành công"));
        }
    }
}

