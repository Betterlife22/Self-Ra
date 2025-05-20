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
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task CreateCategory(CategoryCreateModel categoryCreateModel)
        {
            var category = _mapper.Map<Category>(categoryCreateModel);
            await _unitOfWork.GetRepository<Category>().AddAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<Category>> GetAllCategory()
        {
            var categoriesList = await _unitOfWork.GetRepository<Category>().GetAllAsync();
            //var result =  _mapper.Map<List<CategoryViewModel>>(categoriesList);
            return categoriesList.ToList();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _unitOfWork.GetRepository<Category>().GetByIdAsync(id);
            //var result = _mapper.Map<CategoryViewModel>(category);
            return category;
        }

        public async Task UpdateCategory(CategoryCreateModel categoryCreateModel, Category existedcate)
        {
            _mapper.Map(categoryCreateModel, existedcate);
            await _unitOfWork.GetRepository<Category>().UpdateAsync(existedcate);
            await _unitOfWork.SaveAsync();
        }
    }
}
