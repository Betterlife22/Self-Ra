

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_Core.Constaint;
using Selfra_Core.ExceptionCustom;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.FoodDetailModel;
using Selfra_Services.Infrastructure;
using Selft.Contract.Repositories.Interface;

namespace Selfra_Services.Service
{
    public class FoodDetailService : IFoodDetailService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;
        public FoodDetailService(IMapper mapper , IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _contextAccessor = httpContextAccessor;

        }
        public async Task CreateFoodDetail(CreateFoodDetailModel model)
        {
            Lesson check = await _unitOfWork.GetRepository<Lesson>().Entities.FirstOrDefaultAsync(l => l.Id == model.LessonId && !l.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Lession không tồn tại");

            FoodDetail foodDetail = _mapper.Map<FoodDetail>(model);
            foodDetail.CreatedTime = DateTime.Now;
            foodDetail.CreatedBy = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);
            await _unitOfWork.GetRepository<FoodDetail>().AddAsync(foodDetail);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteFoodDetail(string id)
        {
            FoodDetail foodDetail = await _unitOfWork.GetRepository<FoodDetail>().Entities.FirstOrDefaultAsync(f => f.Id == id && !f.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy fooddetail");

            foodDetail.DeletedTime = DateTime.Now;
            foodDetail.DeletedBy = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);

            await _unitOfWork.GetRepository<FoodDetail>().UpdateAsync(foodDetail);
            await _unitOfWork.SaveAsync();
        }

        public async Task<PaginatedList<ResponseFoodDetailModel>> GetAllFoodDetail(string? searchName, int index, int PageSize)
        {
            IQueryable<ResponseFoodDetailModel> query = from fd  in _unitOfWork.GetRepository<FoodDetail>().Entities
                                                  where !fd.DeletedTime.HasValue
                                                  select new ResponseFoodDetailModel
                                                  {
                                                      LessonId = fd.LessonId,
                                                      FoodDetailId = fd.Id,
                                                      Calories = fd.Calories,
                                                      Country = fd.Country,
                                                      Description = fd.Description,
                                                      FoodName = fd.FoodName,
                                                      ImageUrl = fd.ImageUrl,
                                                      Ingredients = fd.Ingredients,
                                                      MealType = fd.MealType
                                                  };

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                query = query.Where(s => s.FoodName!.Contains(searchName));
            }

            PaginatedList<ResponseFoodDetailModel> paginatedFoodDetail = await _unitOfWork.GetRepository<ResponseFoodDetailModel>().GetPagingAsync(query, index, PageSize);
            return paginatedFoodDetail;
        }

        public async Task<ResponseFoodDetailModel> GetFoodDetailById(string id)
        {
            FoodDetail foodDetail = await _unitOfWork.GetRepository<FoodDetail>().Entities.FirstOrDefaultAsync(f => f.Id == id && !f.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy FoodDetail");

            ResponseFoodDetailModel responseFoodDetailModel = _mapper.Map<ResponseFoodDetailModel>(foodDetail); 

            return responseFoodDetailModel;
        }

        public async Task UpdateFoodDetail(UpdateFoodDetailModel model)
        {
            Lesson check = await _unitOfWork.GetRepository<Lesson>().Entities.FirstOrDefaultAsync(l => l.Id == model.LessonId && !l.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Lession không tồn tại");

            FoodDetail foodDetail = await _unitOfWork.GetRepository<FoodDetail>().Entities.FirstOrDefaultAsync(f => f.Id == model.FoodDetailId && !f.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy fooddetail");

            _mapper.Map(model, foodDetail);

            foodDetail.LastUpdatedTime = DateTime.Now;
            foodDetail.LastUpdatedBy = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);

            await _unitOfWork.GetRepository<FoodDetail>().UpdateAsync(foodDetail);
            await _unitOfWork.SaveAsync();
        }
    }
}
