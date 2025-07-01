using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_ModelViews.Model.PaymentModel
{
    public class PayOSWebhookPayload
    {
        public long orderCode { get; set; }
        public string status { get; set; } = default!;
        public string paymentLinkId { get; set; } = default!;
        public int amount { get; set; }
        public string description { get; set; } = default!;
    }
}
