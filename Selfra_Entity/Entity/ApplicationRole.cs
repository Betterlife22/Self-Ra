

using Microsoft.AspNetCore.Identity;

namespace Selfra_Entity.Model
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        /// <summary>
        /// Tên vai trò
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; }

        /// <summary>
        /// Thời gian lần cuối cập nhật
        /// </summary>
        public DateTimeOffset LastUpdatedTime { get; set; }

        /// <summary>
        /// Thời gian xóa
        /// </summary>
        public DateTimeOffset? DeletedTime { get; set; }
        public ApplicationRole()
        {
            CreatedTime = DateTime.Now;
            LastUpdatedTime = CreatedTime;
            ConcurrencyStamp = Guid.NewGuid().ToString();
        }
    }
}
