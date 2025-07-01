using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class Transaction:BaseEntity
    {
        public string? OrderId { get; set; }
        public string? PaymentLinkId { get; set; }
        public string? PackageId { get; set; }
        public string? UserPackageId { get; set; }
        public decimal Total { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentStatus { get; set; }
        public virtual Package? Package { get; set; }
        public virtual UserPackage? UserPackage { get; set; }

    }
}
