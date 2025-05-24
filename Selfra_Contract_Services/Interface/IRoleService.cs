

using Selfra_Core.Base;
using Selfra_ModelViews.Model.RoleModel;

namespace Selfra_Contract_Services.Interface
{
    public interface IRoleService
    {
        Task<PaginatedList<ResponseRoleModel>> GetAllRole(string? searchName, int index, int PageSize);

        Task CreateRole(CreateRoleModel model);

        Task UpdateRole(UpdateRoleModel model);

        Task DeleteRole(Guid roleId);

        Task<ResponseRoleModel> GetRoleById(Guid roleId);
    }
}
