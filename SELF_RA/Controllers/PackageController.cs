using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_ModelViews.Model.PackageModel;
using Selfra_ModelViews.Model.PostModel;
using Selfra_ModelViews.Model.ZaloModel;
using System.Collections.Generic;

namespace SELF_RA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _packageService;

        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        [HttpPost("CreatePackage")]
        public async Task<IActionResult> CreatePackage(CreatePackageModel model)
        {
            await _packageService.CreatePackage(model);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Tạo mới Package thành công"));
        }

        [HttpGet("GetAllPackage")]
        public async Task<IActionResult> GetAllPackage(string? searchName, int index = 1, int PageSize = 10)
        {
            PaginatedList<ResponsePackageModel> list = await _packageService.GetAllPackage(searchName, index, PageSize);
            return Ok(BaseResponse<ResponsePackageModel>.OkDataResponse(list, "Lấy danh sách thành công"));
        }
        [HttpGet("GetPackageById")]
        public async Task<IActionResult> GetPackageById(string id)
        {
            ResponsePackageModel model = await _packageService.GetPackageById(id);
            return Ok(BaseResponse<ResponsePackageModel>.OkDataResponse(model, "Lấy Package thành công"));
        }

        [HttpPut("UpdatePackage")]
        public async Task<IActionResult> UpdatePost(UpdatePackageModel model)
        {
            await _packageService.UpdatePackage(model);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Cập nhật Package thành công"));
        }

        [HttpDelete("DeletePackage")]
        public async Task<IActionResult> DeletePackage(string id)
        {
            await _packageService.DeletePackage(id);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Xóa Package thành công"));
        }
        [HttpGet("GetAllZaloGroup")]
        public async Task<IActionResult> GetAllZaloGroup()
        {
            var list = await _packageService.GetAllZaloGroup();
            return new OkObjectResult(BaseResponse<List<ZaloViewModel>>.OkDataResponse(list, "Lấy Group thành công"));
        }
        [HttpGet("GetZaloGroupById")]
        public async Task<IActionResult> GetZaloGroupById([FromQuery] string Id)
        {
            var group = await _packageService.GetZaloGroupById(Id);
            return new OkObjectResult(BaseResponse<ZaloViewModel>.OkDataResponse(group, "Lấy Group thành công"));

        }
    }
}
