

using Net.payOS.Types;
using Selfra_ModelViews.Model.PaymentModel;

namespace Selfra_Contract_Services.Interface
{
    public interface IPayMentService
    {
        Task<CreatePaymentResultModel> CreatePaymentLinkAsync(string packageId);

        Task HandlePayOSWebhookAsync(string rawBody, string checksumHeader);
    }
}
