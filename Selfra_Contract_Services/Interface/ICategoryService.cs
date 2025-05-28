using Selfra_ModelViews.Model.CategoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Contract_Services.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAllCategory();
        Task<CategoryViewModel> GetCategoryById(string id);
        Task CreateCategory (CategoryModifyModel model);


    }
}
