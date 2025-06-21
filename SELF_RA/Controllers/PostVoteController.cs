using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_ModelViews.Model.PostModel;
using Selfra_ModelViews.Model.PostVoteModel;

namespace SELF_RA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostVoteController : ControllerBase
    {
        private readonly IPostVoteService _postVoteService;

        public PostVoteController(IPostVoteService postVoteService)
        {
            _postVoteService = postVoteService;
        }

        [HttpPost("CreatePostVote")]
        public async Task<IActionResult> CreatePostVote(CreatePostVoteModel model)
        {
            await _postVoteService.CreatePostVote(model);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Tạo mới PostVote thành công"));
        }

        [HttpGet("GetAllPostVote")]
        public async Task<IActionResult> GetAllPostVote(string? searchPost, int index = 1, int PageSize = 10)
        {
            PaginatedList<ResponsePostVoteModel> list = await _postVoteService.GetAllPostVote(searchPost, index, PageSize);
            return Ok(BaseResponse<ResponsePostVoteModel>.OkDataResponse(list, "Lấy danh sách thành công"));
        }
        [HttpGet("GetPostVoteById")]
        public async Task<IActionResult> GetPostVoteById(string id)
        {
            ResponsePostVoteModel model = await _postVoteService.GetPostVoteById(id);
            return Ok(BaseResponse<ResponsePostVoteModel>.OkDataResponse(model,"Lấy PostVote thành công"));
        }

        [HttpPut("UpdatePostVote")]
        public async Task<IActionResult> UpdatePostVote(UpdatePostVoteModel model)
        {
            await _postVoteService.UpdatePostVote(model);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Cập nhật PostVote thành công"));
        }

        [HttpDelete("DeletePostVote")]
        public async Task<IActionResult> DeletePostVote(string id)
        {
            await _postVoteService.DeletePostVote(id);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Xóa PostVote thành công"));
        }
    }
}
