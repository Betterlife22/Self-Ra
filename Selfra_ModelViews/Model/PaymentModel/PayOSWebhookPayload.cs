using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_ModelViews.Model.PaymentModel
{
    public class PayOSWebhookPayload
    {
        public string? Code { get; set; }
        public string Description { get; set; } = default!;
        public bool Success { get; set; }
        public PayOSData? Data { get; set; }
        public string? Signature { get; set; }
    }
}
