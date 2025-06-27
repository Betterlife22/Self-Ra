using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_ModelViews.Model.CommentVoteModel;
using Selfra_ModelViews.Model.PostModel;

namespace SELF_RA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentVoteController : ControllerBase
    {
        private readonly ICommentVoteService _cmVoteService;
        public CommentVoteController(ICommentVoteService commentVoteService)
        {
            _cmVoteService = commentVoteService;
        }
        [HttpPost("CreateCommentVote")]
        public async Task<IActionResult> CreateCommentVote(CreateCommentVoteModel model)
        {
            await _cmVoteService.CreateCommentVote(model);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Tạo mới CommentVote thành công"));
        }

        [HttpGet("GetAllCommentVote")]
        public async Task<IActionResult> GetAllCommentVote( int index = 1, int PageSize = 10)
        {
            PaginatedList<ResponseCommentVoteModel> list = await _cmVoteService.GetAllCommentVote( index, PageSize);
            return Ok(BaseResponse<ResponseCommentVoteModel>.OkDataResponse(list, "Lấy danh sách thành công"));
        }
        [HttpGet("GetCommentVoteById")]
        public async Task<IActionResult> GetCommentVoteById(string id)
        {
            ResponseCommentVoteModel model = await _cmVoteService.GetCommentVoteById(id);
            return Ok(BaseResponse<ResponseCommentVoteModel>.OkDataResponse(model, "Lấy CommentVote thành công"));
        }

        [HttpPut("UpdateCommentVote")]
        public async Task<IActionResult> UpdateCommentVote(UpdateCommentVoteModel model)
        {
            await _cmVoteService.UpdateCommentVote(model);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Cập nhật CommentVote thành công"));
        }

        [HttpDelete("DeleteCommentVote")]
        public async Task<IActionResult> DeleteCommentVote(string id)
        {
            await _cmVoteService.DeleteCommentVote(id);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Xóa CommentVote thành công"));
        }

        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAll()
        {
            await _cmVoteService.DeleteAll();
            return Ok(BaseResponse<string>.OkMessageResponseModel("Xóa CommentVote thành công"));
        }
    }
}

