
using Microsoft.AspNetCore.Mvc;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_ModelViews.Model.ForumModel;

namespace SELF_RA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumCommentController : ControllerBase
    {
        private readonly IForumCommentService _forumService;

        public ForumCommentController(IForumCommentService forumService)
        {
            _forumService = forumService;
        }

        [HttpPost("CreateForumComment")]
        public async Task<IActionResult> CreateForumComment(CreateForumModel model)
        {
            await _forumService.CreateForumComment(model);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Tạo mới ForumComment thành công"));
        }

        [HttpGet("GetAllForumComment")]
        public async Task<IActionResult> GetAllForumComment(string? searchPost, int index = 1, int PageSize = 10)
        {
            PaginatedList<ResponseForumComment> list = await _forumService.GetAllForumComment(searchPost, index, PageSize);
            return Ok(BaseResponse<ResponseForumComment>.OkDataResponse(list, "Lấy danh sách thành công"));
        }
        [HttpGet("GetForumCommentById")]
        public async Task<IActionResult> GetForumCommentById(string id)
        {
            ResponseForumComment model = await _forumService.GetForumCommentById(id);
            return Ok(BaseResponse<ResponseForumComment>.OkDataResponse(model,"Lấy PostVote thành công"));
        }

        [HttpPut("UpdateForumComment")]
        public async Task<IActionResult> UpdateForumComment(UpdateForumModel model)
        {
            await _forumService.UpdateForumComment(model);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Cập nhật ForumComment thành công"));
        }

        [HttpDelete("DeleteForumComment")]
        public async Task<IActionResult> DeleteForumComment(string id)
        {
            await _forumService.DeleteForumComment(id);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Xóa ForumComment thành công"));
        }
    }
}
