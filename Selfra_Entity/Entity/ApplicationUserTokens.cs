using Microsoft.AspNetCore.Identity;

namespace Selfra_Entity.Model
{
    public class ApplicationUserTokens : IdentityUserToken<Guid>
    {
        public DateTimeOffset CreatedTime { get; set; }

        public DateTimeOffset LastUpdatedTime { get; set; }

        public DateTimeOffset? DeletedTime { get; set; }
        public ApplicationUserTokens()
        {
            CreatedTime = DateTime.Now;
            LastUpdatedTime = CreatedTime;
        }
    }
}
