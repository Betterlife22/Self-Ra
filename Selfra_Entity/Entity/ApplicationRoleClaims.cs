

using Microsoft.AspNetCore.Identity;

namespace Selfra_Entity.Model
{
    public class ApplicationRoleClaims : IdentityRoleClaim<Guid>
    {
        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; }

        /// <summary>
        /// Thời gian cập nhật cuối cùng
        /// </summary>
        public DateTimeOffset LastUpdatedTime { get; set; }

        /// <summary>
        /// Thời gian xóa
        /// </summary>
        public DateTimeOffset? DeletedTime { get; set; }
        public ApplicationRoleClaims()
        {
            CreatedTime = DateTime.Now;
            LastUpdatedTime = CreatedTime;
        }
    }
}
