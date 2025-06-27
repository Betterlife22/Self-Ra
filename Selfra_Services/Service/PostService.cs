
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_Core.Constaint;
using Selfra_Core.ExceptionCustom;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.PostModel;
using Selfra_Services.Infrastructure;
using Selft.Contract.Repositories.Interface;

namespace Selfra_Services.Service
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = httpContextAccessor;
        }

        public async Task CreatePost(CreatePostModel model)
        {
            ApplicationUser user = await _unitOfWork.GetRepository<ApplicationUser>().Entities.FirstOrDefaultAsync(u => u.Id == model.UserId && !u.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "User Không tồn tại");

            Post post = _mapper.Map<Post>(model);
            post.CreatedBy = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);
            post.CreatedTime = DateTime.Now;

            await _unitOfWork.GetRepository<Post>().AddAsync(post);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeletePost(string id)
        {
            Post check = await _unitOfWork.GetRepository<Post>().Entities.FirstOrDefaultAsync(c => c.Id == id)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy Post");

            check.DeletedBy = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);
            check.DeletedTime = DateTime.Now;

            await _unitOfWork.GetRepository<Post>().UpdateAsync(check);
            await _unitOfWork.SaveAsync();
        }

        public async Task<PaginatedList<ResponsePostModel>> GetAllPost(string? searchName, int index, int PageSize)
        {
            IQueryable<ResponsePostModel> query = from post in _unitOfWork.GetRepository<Post>().Entities
                                                  where !post.DeletedTime.HasValue
                                                  select new ResponsePostModel
                                                  {
                                                      PostId = post.Id,
                                                      UserId = post.UserId,
                                                      Content = post.Content,
                                                      Title = post.Title,
                                                      IsActive = post.IsActive,
                                                      Category = post.CategoryPost,
                                                      ArticleUrl = post.ArticleUrl,
                                                      ImageUrl = post.ImageUrl,
                                                      
                                                  };

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                query = query.Where(s => s.Title!.Contains(searchName));
            }

            PaginatedList<ResponsePostModel> paginatedPost = await _unitOfWork.GetRepository<ResponsePostModel>().GetPagingAsync(query, index, PageSize);
            return paginatedPost;
        }

        public async Task<ResponsePostModel> GetPostById(string id)
        {
            Post check = await _unitOfWork.GetRepository<Post>().Entities.FirstOrDefaultAsync(c => c.Id == id)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy Post");
        
            ResponsePostModel model = _mapper.Map<ResponsePostModel>(check);
            
            return model;
        }

        public async Task UpdatePost(UpdatePostModel model)
        {
            Post check = await _unitOfWork.GetRepository<Post>().Entities.FirstOrDefaultAsync(c => c.Id == model.PostId)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy Post");

            ApplicationUser user = await _unitOfWork.GetRepository<ApplicationUser>().Entities.FirstOrDefaultAsync(u => u.Id == model.UserId && !u.DeletedTime.HasValue)
                    ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "User Không tồn tại");

            _mapper.Map(model, check);

            check.LastUpdatedTime = DateTime.Now;
            check.LastUpdatedBy = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);

            await _unitOfWork.GetRepository<Post>().UpdateAsync(check);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAll()
        {
            IEnumerable<Post> all = await _unitOfWork.GetRepository<Post>().GetAllAsync();
            foreach(Post post in all)
            {
                await _unitOfWork.GetRepository<Post>().DeleteAsync(post.Id);
            }
            
            await _unitOfWork.SaveAsync();
        }
    }
}
