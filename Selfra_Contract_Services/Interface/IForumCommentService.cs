

using Selfra_Core.Base;
using Selfra_ModelViews.Model.ForumModel;

namespace Selfra_Contract_Services.Interface
{
    public interface IForumCommentService
    {
        Task CreateForumComment(CreateForumModel model);

        Task UpdateForumComment(UpdateForumModel model);

        Task DeleteForumComment(string id);

        Task<ResponseForumComment> GetForumCommentById(string id);

        public Task DeleteAll();
        Task<PaginatedList<ResponseForumComment>> GetAllForumComment(string? searchName, int index, int PageSize);
    }
}
