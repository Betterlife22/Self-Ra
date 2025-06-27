

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_Core.Constaint;
using Selfra_Core.ExceptionCustom;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.CommentVoteModel;
using Selfra_Services.Infrastructure;
using Selft.Contract.Repositories.Interface;

namespace Selfra_Services.Service
{
    public class CommentVoteService : ICommentVoteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        public CommentVoteService(IUnitOfWork unitOfWork , IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task CreateCommentVote(CreateCommentVoteModel model)
        {
            ApplicationUser user = await _unitOfWork.GetRepository<ApplicationUser>().Entities.FirstOrDefaultAsync(u => u.Id == model.UserId && !u.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy User");

            ForumComment fr = await _unitOfWork.GetRepository<ForumComment>().Entities.FirstOrDefaultAsync(f=>f.Id == model.CommentId && !f.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy ForumComment");

            CommentVote commentVote = _mapper.Map<CommentVote>(model);
            commentVote.CreatedBy = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);
            commentVote.CreatedTime = DateTime.Now;

            await _unitOfWork.GetRepository<CommentVote>().AddAsync(commentVote);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAll()
        {
            IEnumerable<CommentVote> all = await _unitOfWork.GetRepository<CommentVote>().GetAllAsync();
            foreach (CommentVote fr in all)
            {
                await _unitOfWork.GetRepository<CommentVote>().DeleteAsync(fr.Id);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteCommentVote(string id)
        {
            CommentVote commentVote = await _unitOfWork.GetRepository<CommentVote>().Entities.FirstOrDefaultAsync(c=>c.Id == id && !c.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy CommentVote");

            commentVote.DeletedBy = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);
            commentVote.DeletedTime = DateTime.Now;

            await _unitOfWork.GetRepository<CommentVote>().UpdateAsync(commentVote);
            await _unitOfWork.SaveAsync();
        }

        public async Task<PaginatedList<ResponseCommentVoteModel>> GetAllCommentVote(int index, int PageSize)
        {
            IQueryable<ResponseCommentVoteModel> query = from cmvote in _unitOfWork.GetRepository<CommentVote>().Entities
                                                  where !cmvote.DeletedTime.HasValue
                                                  select new ResponseCommentVoteModel
                                                  {
                                                      CommentId = cmvote.CommentId,
                                                      CommentVoteId = cmvote.Id,
                                                      UserId = cmvote.UserId,
                                                      VoteValue = cmvote.VoteValue,

                                                  };


            PaginatedList<ResponseCommentVoteModel> paginatedcmVote = await _unitOfWork.GetRepository<ResponseCommentVoteModel>().GetPagingAsync(query, index, PageSize);
            return paginatedcmVote;
        }

        public async Task<ResponseCommentVoteModel> GetCommentVoteById(string id)
        {

            CommentVote commentVote = await _unitOfWork.GetRepository<CommentVote>().Entities.FirstOrDefaultAsync(c => c.Id == id && !c.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy CommentVote");

            ResponseCommentVoteModel commentVoteModel = _mapper.Map<ResponseCommentVoteModel>(commentVote);
            return commentVoteModel;
        }

        public async Task UpdateCommentVote(UpdateCommentVoteModel model)
        {
            CommentVote commentVote = await _unitOfWork.GetRepository<CommentVote>().Entities.FirstOrDefaultAsync(c => c.Id == model.CommentVoteId && !c.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy CommentVote");

            _mapper.Map(model, commentVote);

            commentVote.LastUpdatedTime = DateTime.Now;
            commentVote.LastUpdatedBy = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);

            await _unitOfWork.GetRepository<CommentVote>().UpdateAsync(commentVote);
            await _unitOfWork.SaveAsync();
        }
    }
}
