using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class UserPackage:BaseEntity
    {
        public string? UserId { get; set; }
        public string? PackageId { get; set; }
        public string? Status { get; set; }
        public virtual Package? Package { get; set; }

    }
}
