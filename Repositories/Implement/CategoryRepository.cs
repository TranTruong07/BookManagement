using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implement
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookManagementContext context;
        public CategoryRepository()
        {
            this.context = new BookManagementContext();
        }
        public List<Category> GetAllCategory()
        {
            return context.Categories.ToList();
        }
    }
}
