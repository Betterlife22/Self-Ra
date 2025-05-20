using Selfra_Entity.Model;
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
        public Task<List<Category>> GetAllCategory();
        public Task<Category> GetCategoryById(int id);
        public Task CreateCategory(CategoryCreateModel categoryCreateModel);
        public Task UpdateCategory(CategoryCreateModel categoryCreateModel, Category existedcate);

    }
}
