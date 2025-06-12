

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_Core.Constaint;
using Selfra_Core.ExceptionCustom;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.PostVoteModel;
using Selfra_Services.Infrastructure;
using Selft.Contract.Repositories.Interface;

namespace Selfra_Services.Service
{
    public class PostVoteService : IPostVoteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public PostVoteService(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
        }

        public async Task CreatePostVote(CreatePostVoteModel model)
        {
            Post check = await _unitOfWork.GetRepository<Post>().Entities.FirstOrDefaultAsync(c => c.Id == model.PostId && !c.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy Post");

            ApplicationUser user = await _unitOfWork.GetRepository<ApplicationUser>().Entities.FirstOrDefaultAsync(u => u.Id == model.UserId && !u.DeletedTime.HasValue)
            ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy user");

            PostVote postVote = _mapper.Map<PostVote>(model);
            postVote.CreatedBy = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);
            postVote.CreatedTime = DateTime.Now;

            await _unitOfWork.GetRepository<PostVote>().AddAsync(postVote);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeletePostVote(string id)
        {
            PostVote postVote = await _unitOfWork.GetRepository<PostVote>().Entities.FirstOrDefaultAsync(p => p.Id == id && !p.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy PostVote");

            postVote.DeletedTime = DateTime.Now;
            postVote.DeletedBy = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);

            await _unitOfWork.GetRepository<PostVote>().UpdateAsync(postVote);
            await _unitOfWork.SaveAsync();
        }

        public async Task<PaginatedList<ResponsePostVoteModel>> GetAllPostVote(string? searchPost, int index, int PageSize)
        {
            IQueryable<ResponsePostVoteModel> query = from postVote in _unitOfWork.GetRepository<PostVote>().Entities
                                                      where !postVote.DeletedTime.HasValue
                                                      select new ResponsePostVoteModel
                                                      {
                                                          PostId = postVote.PostId,
                                                          PostVoteId = postVote.Id,
                                                          UserId = postVote.UserId,
                                                          VoteValue = postVote.VoteValue,
                                                      };


            if (!string.IsNullOrWhiteSpace(searchPost))
            {
                query = query.Where(s => s.PostId!.Contains(searchPost));
            }

            PaginatedList<ResponsePostVoteModel> paginatePostVote = await _unitOfWork.GetRepository<ResponsePostVoteModel>().GetPagingAsync(query, index, PageSize);

            return paginatePostVote;
        }

        public async Task<ResponsePostVoteModel> GetPostVoteById(string id)
        {
            PostVote postVote = await _unitOfWork.GetRepository<PostVote>().Entities.FirstOrDefaultAsync(p => p.Id == id && !p.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy PostVote");

            ResponsePostVoteModel postVoteModel = _mapper.Map<ResponsePostVoteModel>(postVote);
            return postVoteModel;
        }

        public async Task UpdatePostVote(UpdatePostVoteModel model)
        {
            PostVote postVote = await _unitOfWork.GetRepository<PostVote>().Entities.FirstOrDefaultAsync(p => p.Id == model.PostVoteId && !p.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy PostVote");

            _mapper.Map(model, postVote);

            postVote.LastUpdatedTime = DateTime.Now;
            postVote.LastUpdatedBy = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);

            await _unitOfWork.GetRepository<PostVote>().UpdateAsync(postVote);
            await _unitOfWork.SaveAsync();           
        }
    }
}
