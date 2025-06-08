

using Selfra_Core.Base;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.FoodDetailModel;
using Selfra_ModelViews.Model.RoleModel;

namespace Selfra_Contract_Services.Interface
{
    public interface IFoodDetailService
    {
        Task CreateFoodDetail(CreateFoodDetailModel model);

        Task UpdateFoodDetail(UpdateFoodDetailModel model);

        Task DeleteFoodDetail(string id);

        Task<ResponseFoodDetailModel> GetFoodDetailById (string id);

        Task<PaginatedList<ResponseFoodDetailModel>> GetAllFoodDetail(string? searchName, int index, int PageSize);
    }
}
