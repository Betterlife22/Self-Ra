using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.CategoryModel;

namespace SELF_RA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper
        
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            try
            {
                var category = await _categoryService.GetAllCategory();
                var result = _mapper.Map<List<CategoryViewModel>>(category);

                var response = BaseResponseModel<CategoryViewModel>.OkDataResponse(result, "Load success");

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
        [HttpGet("GetCategoryByID")]
        public async Task<IActionResult> GetCategoryByID([FromQuery] int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryById(id);
                if (category == null)
                {
                    return new NotFoundObjectResult(BaseResponseModel<CategoryViewModel>.NotFoundResponseModel(null, "Category Not Found"));
                }
                var result = _mapper.Map<CategoryViewModel>(category);  
                var response = BaseResponseModel<CategoryViewModel>.OkDataResponse(result, "Load success");

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

        }
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateModel category)
        {
            await _categoryService.CreateCategory(category);
            
            var response = BaseResponseModel<CategoryViewModel>.OkDataResponse(category,"Create success");

            return new OkObjectResult(response);
        }
        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromQuery] int id, [FromBody] CategoryCreateModel category)
        {
            var existed = await _categoryService.GetCategoryById(id);
            if(existed == null)
            {
                return new NotFoundObjectResult(BaseResponseModel<CategoryViewModel>.NotFoundResponseModel(null, "Category Not Found"));
            }
            await _categoryService.UpdateCategory(category, existed);
            return NoContent();
        }
    }
}
