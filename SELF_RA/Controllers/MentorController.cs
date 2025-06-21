using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_ModelViews.Model.MentorModel;
using Selfra_ModelViews.Model.PostModel;

namespace SELF_RA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentorController : ControllerBase
    {
        private readonly IMentorService _mentorService;

        public MentorController(IMentorService mentorService)
        {
            _mentorService = mentorService;
        }

        [HttpPost("CreateMentor")]
        public async Task<IActionResult> CreateMentor(CreateMentorModel model)
        {
            await _mentorService.CreateMentor(model);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Tạo mới Mentor thành công"));
        }

        [HttpGet("GetAllMentor")]
        public async Task<IActionResult> GetAllMentor(string? searchName, int index = 1, int PageSize = 10)
        {
            PaginatedList<ResponseMentorModel> list = await _mentorService.GetAllMentor(searchName, index, PageSize);
            return Ok(BaseResponse<ResponseMentorModel>.OkDataResponse(list, "Lấy danh sách thành công"));
        }
        [HttpGet("GetMentorById")]
        public async Task<IActionResult> GetMentorById(string id)
        {
            ResponseMentorModel model = await _mentorService.GetMentorById(id);
            return Ok(BaseResponse<ResponseMentorModel>.OkDataResponse(model, "Lấy Mentor thành công"));
        }

        [HttpPut("UpdateMentor")]
        public async Task<IActionResult> UpdateMentor(UpdateMentorModel model)
        {
            await _mentorService.UpdateMentor(model);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Cập nhật Mentor thành công"));
        }

        [HttpDelete("DeleteMentor")]
        public async Task<IActionResult> DeleteMentor(string id)
        {
            await _mentorService.DeleteMentor(id);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Xóa Mentor thành công"));
        }
    }
}

