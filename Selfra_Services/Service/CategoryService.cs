using AutoMapper;
using Selfra_Contract_Services.Interface;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.CategoryModel;
using Selft.Contract.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Services.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task CreateCategory(CategoryModifyModel model)
        {
            var category = _mapper.Map<Category>(model);
            category.CreatorId = Guid.Parse(model.CreatorId);
            await _unitOfWork.GetRepository<Category>().AddAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<CategoryViewModel>> GetAllCategory()
        {
            var CategoryList = await _unitOfWork.GetRepository<Category>().GetAllAsync();
            var result = _mapper.Map<List<CategoryViewModel>>(CategoryList); 
            return result;
        }

        public async Task<CategoryViewModel> GetCategoryById(string id)
        {
            var category = await _unitOfWork.GetRepository<Category>().GetByIdAsync(id);
            if(category == null)
            {
                return null;
            }
            var result = _mapper.Map<CategoryViewModel>(category);
            return result;
        }
    }
}
