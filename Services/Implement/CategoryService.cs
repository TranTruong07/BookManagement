using DataAccess.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public int AddCategory(Category category)
        {
            return categoryRepository.AddCategory(category);
        }

        public List<Category> GetAllCategory()
        {
            return categoryRepository.GetAllCategory();
        }
    }
}
