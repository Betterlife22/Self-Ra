

using Selfra_Core.Base;
using Selfra_ModelViews.Model.FoodDetailModel;
using Selfra_ModelViews.Model.PostModel;

namespace Selfra_Contract_Services.Interface
{
    public interface IPostService
    {
        Task CreatePost(CreatePostModel model);

        Task UpdatePost(UpdatePostModel model);

        Task DeletePost(string id);

        Task<ResponsePostModel> GetPostById(string id);

        Task<PaginatedList<ResponsePostModel>> GetAllPost(string? searchName, int index, int PageSize);
    }
}
