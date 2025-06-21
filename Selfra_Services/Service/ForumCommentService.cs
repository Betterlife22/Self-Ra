

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_Core.Constaint;
using Selfra_Core.ExceptionCustom;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.ForumModel;
using Selfra_ModelViews.Model.PostModel;
using Selfra_Services.Infrastructure;
using Selft.Contract.Repositories.Interface;

namespace Selfra_Services.Service
{
    public class ForumCommentService : IForumCommentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;

        public ForumCommentService(IMapper mapper , IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task CreateForumComment(CreateForumModel model)
        {
            Post post = await _unitOfWork.GetRepository<Post>().Entities.FirstOrDefaultAsync(p => p.Id == model.PostId && !p.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy Post");

            ApplicationUser applicationUser = await _unitOfWork.GetRepository<ApplicationUser>().Entities.FirstOrDefaultAsync(a => a.Id == model.UserId && !a.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy User");

            ForumComment forumComment = _mapper.Map<ForumComment>(model);
            forumComment.CreatedBy = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);
            forumComment.CreatedTime = DateTime.UtcNow;

            await _unitOfWork.GetRepository<ForumComment>().AddAsync(forumComment);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAll()
        {
            IEnumerable<ForumComment> all = await _unitOfWork.GetRepository<ForumComment>().GetAllAsync();
            foreach (ForumComment fr in all)
            {
                await _unitOfWork.GetRepository<ForumComment>().DeleteAsync(fr.Id);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteForumComment(string id)
        {
            ForumComment check = await _unitOfWork.GetRepository<ForumComment>().Entities.FirstOrDefaultAsync(c => c.Id == id && !c.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy ForumComment");

            check.DeletedTime = DateTime.Now;
            check.DeletedBy = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);

            await _unitOfWork.GetRepository<ForumComment>().UpdateAsync(check);
            await _unitOfWork.SaveAsync();
        }

        public async Task<PaginatedList<ResponseForumComment>> GetAllForumComment(string? searchName, int index, int PageSize)
        {
            IQueryable<ResponseForumComment> query = from fr in _unitOfWork.GetRepository<ForumComment>().Entities
                                                  where !fr.DeletedTime.HasValue
                                                  select new ResponseForumComment
                                                  {
                                                      PostId = fr.PostId,
                                                      Content = fr.Content,
                                                      ForumCommentId = fr.Id,
                                                      UserId = fr.UserId,

                                                  };

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                query = query.Where(s => s.Content!.Contains(searchName));
            }

            PaginatedList<ResponseForumComment> paginatedForum = await _unitOfWork.GetRepository<ResponseForumComment>().GetPagingAsync(query, index, PageSize);
            return paginatedForum;
        }

        public async Task<ResponseForumComment> GetForumCommentById(string id)
        {
            ForumComment check = await _unitOfWork.GetRepository<ForumComment>().Entities.FirstOrDefaultAsync(c => c.Id == id && !c.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy ForumComment");

            ResponseForumComment responseForumComment = _mapper.Map<ResponseForumComment>(check);
            return responseForumComment;
        }

        public async Task UpdateForumComment(UpdateForumModel model)
        {
            ForumComment check = await _unitOfWork.GetRepository<ForumComment>().Entities.FirstOrDefaultAsync(c => c.Id == model.ForumCommentId && !c.DeletedTime.HasValue)
               ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy ForumComment");
            Post post = await _unitOfWork.GetRepository<Post>().Entities.FirstOrDefaultAsync(p => p.Id == model.PostId && !p.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy Post");

            ApplicationUser applicationUser = await _unitOfWork.GetRepository<ApplicationUser>().Entities.FirstOrDefaultAsync(a => a.Id == model.UserId && !a.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy User");

            _mapper.Map(model, check);

            check.LastUpdatedTime = DateTime.Now;
            check.LastUpdatedBy = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);

            await _unitOfWork.GetRepository<ForumComment>().UpdateAsync(check);
            await _unitOfWork.SaveAsync();
        }
    }
}
