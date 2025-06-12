
using Selfra_Core.Base;
using Selfra_ModelViews.Model.PostModel;
using Selfra_ModelViews.Model.PostVoteModel;

namespace Selfra_Contract_Services.Interface
{
    public interface IPostVoteService
    {
        Task CreatePostVote(CreatePostVoteModel model);

        Task UpdatePostVote(UpdatePostVoteModel model);

        Task DeletePostVote(string id);

        Task<ResponsePostVoteModel> GetPostVoteById(string id);

        Task<PaginatedList<ResponsePostVoteModel>> GetAllPostVote(string? searhcPost, int index, int PageSize);
    }
}
