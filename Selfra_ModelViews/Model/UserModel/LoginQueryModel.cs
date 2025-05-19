

using Selfra_Entity.Model;

namespace Selfra_ModelViews.Model.UserModel
{
    public class LoginQueryModel
    {
        public ApplicationUser? User { get; set; }

        public string? RoleName { get; set; }
    }
}
