using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_Services.Service;

namespace SELF_RA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsAIController : ControllerBase
    {
        private readonly INewsUpdateService _newsService;

        public NewsAIController(INewsUpdateService newsUpdateService)
        {
            _newsService = newsUpdateService;
        }

        [HttpPost]
        public async Task<IActionResult> ExcuteNewsAi()
        {
            await _newsService.UpdateNewsAsync();
            return Ok(BaseResponse<string>.OkMessageResponseModel("Lấy Post thành công"));
        }
    }
}
