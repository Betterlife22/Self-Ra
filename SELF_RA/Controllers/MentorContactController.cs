using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_ModelViews.Model.MentorContact;
using Selfra_ModelViews.Model.MentorModel;

namespace SELF_RA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentorContactController : ControllerBase
    {
        private readonly IMentorContactService _mentorContactService;

        public MentorContactController(IMentorContactService mentorContactService)
        {
            _mentorContactService = mentorContactService;
        }

        [HttpPost("CreateMentorContact")]
        public async Task<IActionResult> CreateMentorContact(CreateMentorContact model)
        {
            await _mentorContactService.CreateMentorContact(model);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Tạo mới MentorContact thành công"));
        }

        [HttpGet("GetAllMentorContact")]
        public async Task<IActionResult> GetAllMentorContact(string? searchName, int index = 1, int PageSize = 10)
        {
            PaginatedList<ResponseMentorContact> list = await _mentorContactService.GetAllMentorContact(searchName, index, PageSize);
            return Ok(BaseResponse<ResponseMentorContact>.OkDataResponse(list, "Lấy danh sách thành công"));
        }
        [HttpGet("GetMentorContactById")]
        public async Task<IActionResult> GetMentorContactById(string id)
        {
            ResponseMentorContact model = await _mentorContactService.GetMentorContactById(id);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Lấy MentorContact thành công"));
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

