﻿using Selfra_Core.Base;
using Selfra_ModelViews.Model.PackageModel;
using Selfra_ModelViews.Model.PostModel;
using Selfra_ModelViews.Model.ZaloModel;

namespace Selfra_Contract_Services.Interface
{
    public interface IPackageService
    {
        Task CreatePackage(CreatePackageModel model);

        Task UpdatePackage(UpdatePackageModel model);

        Task DeletePackage(string id);

        Task<ResponsePackageModel> GetPackageById(string id);

        Task<PaginatedList<ResponsePackageModel>> GetAllPackage(string? searchName, int index, int PageSize);
        Task<List<ZaloViewModel>> GetAllZaloGroup();
        Task<ZaloViewModel> GetZaloGroupById (string id);
    }
}
