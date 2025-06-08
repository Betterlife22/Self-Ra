using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_ModelViews.Model.FoodDetailModel;
using Selfra_ModelViews.Model.PostModel;

namespace SELF_RA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost(CreatePostModel model)
        {
            await _postService.CreatePost(model);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Tạo mới Post thành công"));
        }

        [HttpGet("GetAllPost")]
        public async Task<IActionResult> GetAllPost(string? searchName, int index = 1, int PageSize = 10)
        {
            PaginatedList<ResponsePostModel> list = await _postService.GetAllPost(searchName, index, PageSize);
            return Ok(BaseResponse<ResponsePostModel>.OkDataResponse(list, "Lấy danh sách thành công"));
        }
        [HttpGet("GetPostById")]
        public async Task<IActionResult> GetFoodDetailById(string id)
        {
            ResponsePostModel model = await _postService.GetPostById(id);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Lấy Post thành công"));
        }

        [HttpPut("UpdatePost")]
        public async Task<IActionResult> UpdatePost(UpdatePostModel model)
        {
            await _postService.UpdatePost(model);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Cập nhật Post thành công"));
        }

        [HttpDelete("DeletePost")]
        public async Task<IActionResult> DeletePost(string id)
        {
            await _postService.DeletePost(id);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Xóa Post thành công"));
        }
    }
}
