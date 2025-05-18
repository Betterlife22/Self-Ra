
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Selfra_Entity.Model;

namespace Selfra_Repositories.Base
{
    public class SelfraDBContext : IdentityDbContext <ApplicationUser, ApplicationRole, Guid,ApplicationUserClaims, ApplicationUserRoles,ApplicationUserLogins ,ApplicationRoleClaims, ApplicationUserTokens>
    {
        public SelfraDBContext(DbContextOptions<SelfraDBContext> options) : base(options) {

        }

        public virtual DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
        public virtual DbSet<ApplicationRole> ApplicationRoles => Set<ApplicationRole>();
        public virtual DbSet<ApplicationUserClaims> ApplicationUserClaims => Set<ApplicationUserClaims>();
        public virtual DbSet<ApplicationUserRoles> ApplicationUserRoles => Set<ApplicationUserRoles>();
        public virtual DbSet<ApplicationUserLogins> ApplicationUserLogins => Set<ApplicationUserLogins>();
        public virtual DbSet<ApplicationRoleClaims> ApplicationRoleClaims => Set<ApplicationRoleClaims>();
        public virtual DbSet<ApplicationUserTokens> ApplicationUserTokens => Set<ApplicationUserTokens>();
    }
}
