

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_Core.Constaint;
using Selfra_Core.ExceptionCustom;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.PackageModel;
using Selfra_ModelViews.Model.PostModel;
using Selfra_Services.Infrastructure;
using Selft.Contract.Repositories.Interface;

namespace Selfra_Services.Service
{
    public class PackageService : IPackageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public PackageService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreatePackage(CreatePackageModel model)
        {
            Package package = _mapper.Map<Package>(model);

            package.CreatedBy = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);
            package.CreatedTime = DateTime.Now;

            await _unitOfWork.GetRepository<Package>().AddAsync(package);
            await _unitOfWork.SaveAsync();
        }


        public async Task DeletePackage(string id)
        {
            Package package = await _unitOfWork.GetRepository<Package>().Entities.FirstOrDefaultAsync(p => p.Id == id && !p.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy Package");

            package.DeletedTime = DateTime.Now;
            package.DeletedBy = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);

            await _unitOfWork.GetRepository<Package>().UpdateAsync(package);
            await _unitOfWork.SaveAsync();
        }

        public async Task<PaginatedList<ResponsePackageModel>> GetAllPackage(string? searchName, int index, int PageSize)
        {
            IQueryable<ResponsePackageModel> query = from package in _unitOfWork.GetRepository<Package>().Entities
                                                  where !package.DeletedTime.HasValue
                                                  select new ResponsePackageModel
                                                  {
                                                      Description = package.Description,
                                                      Duration = package.Duration,
                                                      IsPublic = package.IsPublic,
                                                      Name = package.Name,
                                                      PackageId = package.Id,
                                                      Price = package.Price
                                                  };

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                query = query.Where(s => s.Name!.Contains(searchName));
            }

            PaginatedList<ResponsePackageModel> paginatedpackage = await _unitOfWork.GetRepository<ResponsePackageModel>().GetPagingAsync(query, index, PageSize);
            return paginatedpackage;
        }

        public async Task<ResponsePackageModel> GetPackageById(string id)
        {
            Package check = await _unitOfWork.GetRepository<Package>().Entities.FirstOrDefaultAsync(p => p.Id == id && !p.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy Package");

            ResponsePackageModel model = _mapper.Map<ResponsePackageModel>(check);

            return model;
        }

        public async Task UpdatePackage(UpdatePackageModel model)
        {
            Package check = await _unitOfWork.GetRepository<Package>().Entities.FirstOrDefaultAsync(p => p.Id == model.PackageId && !p.DeletedTime.HasValue)
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.BADREQUEST, "Không tìm thấy Package");

            _mapper.Map(model, check);

            check.LastUpdatedTime = DateTime.Now;
            check.LastUpdatedBy = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);

            await _unitOfWork.GetRepository<Package>().UpdateAsync(check);
            await _unitOfWork.SaveAsync();
        }
    }
}
