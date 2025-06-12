
using Microsoft.AspNetCore.Mvc;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_ModelViews.Model.FoodDetailModel;

namespace SELF_RA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodDetailController : ControllerBase
    {
        private readonly IFoodDetailService _foodDetailService;

        public FoodDetailController(IFoodDetailService foodDetailService) {
            _foodDetailService = foodDetailService;
        }

        [HttpPost("CreateFoodDetail")]
        public async Task<IActionResult> CreateFoodDetail(CreateFoodDetailModel model)
        {
            await _foodDetailService.CreateFoodDetail(model);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Tạo mới FoodDetail"));
        }

        [HttpGet("GetAllFoodDetails")]
        public async Task<IActionResult> GetAllFoodDetails(string? searchName, int index = 1, int PageSize = 10)
        {
            PaginatedList<ResponseFoodDetailModel> list = await _foodDetailService.GetAllFoodDetail(searchName, index, PageSize);
            return Ok(BaseResponse<ResponseFoodDetailModel>.OkDataResponse(list, "Lấy danh sách thành công"));
        }
        [HttpGet("GetFoodDetailById")]
        public async Task<IActionResult> GetFoodDetailById(string id)
        {
            ResponseFoodDetailModel model = await _foodDetailService.GetFoodDetailById(id);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Lấy foodetail thành công"));
        }

        [HttpPut("UpdateFoodDetail")]
        public async Task<IActionResult> UpdateFoodDetail(UpdateFoodDetailModel model)
        {
            await _foodDetailService.UpdateFoodDetail(model);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Cập nhật fooddetail thành công"));
        }

        [HttpDelete("DeleteFoodDetail")]
        public async Task<IActionResult> DeleteFoodDetail(string id)
        {
            await _foodDetailService.DeleteFoodDetail(id);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Xóa FoodDetail thành công"));
        }
    }
}
