
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_ModelViews.Model.PaymentModel;
using Selfra_Services.Service;
using System.Text;

namespace SELF_RA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayMentService _payMentService;
        private readonly ILogger _logger;
        public PaymentController(IPayMentService payMentService, ILogger logger)
        {
            _payMentService = payMentService;
            _logger = logger;
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
            using var reader = new StreamReader(Request.Body);
            var rawBody = await reader.ReadToEndAsync();
        
            JObject obj = JObject.Parse(rawBody);
            JObject data = (JObject)obj["data"]!;
            string signature = obj["signature"]!.ToString();
            var isValid = _payMentService.IsValidData(data.ToString(), signature);

            if (!isValid)
            {
                return BadRequest("Invalid data signature");
            }
            try
            {
                await _payMentService.HandlePayOSWebhookAsync(rawBody);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            
            return Ok(new { success = true }); 
        }
    }
}
