using Selfra_Core.Base;
using Selfra_ModelViews.Model.CommentVoteModel;
using Selfra_ModelViews.Model.PostModel;


namespace Selfra_Contract_Services.Interface
{
    public interface ICommentVoteService
    {
        Task CreateCommentVote(CreateCommentVoteModel model);

        Task UpdateCommentVote(UpdateCommentVoteModel model);

        Task DeleteCommentVote(string id);

        Task<ResponseCommentVoteModel> GetCommentVoteById(string id);

        public Task DeleteAll();
        Task<PaginatedList<ResponseCommentVoteModel>> GetAllCommentVote( int index, int PageSize);
    }
}
