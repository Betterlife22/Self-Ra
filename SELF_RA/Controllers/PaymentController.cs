
using Microsoft.AspNetCore.Mvc;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_ModelViews.Model.PaymentModel;

namespace SELF_RA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayMentService _payMentService;

        public PaymentController(IPayMentService payMentService)
        {
            _payMentService = payMentService;
        }

        [HttpPost("CreateLinkPayMent")]
        public async Task<IActionResult> CreateLinkPayMent(string PackageId)
        {
            CreatePaymentResultModel result = await _payMentService.CreatePaymentLinkAsync(PackageId);
            return Ok(BaseResponse<string>.OkDataResponse(result, "Tạo link thành công"));
        }
        [HttpPost("webhook/payos")]
        public async Task<IActionResult> Webhook()
        {
            var rawBody = Request.Body.ToString()!;
            var checksum = Request.Headers["x-checksum"].ToString();

            await _payMentService.HandlePayOSWebhookAsync(rawBody, checksum);
            return Ok(BaseResponse<string>.OkMessageResponseModel("Thanh toán thanh cong"));

        }
    }
}
