using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implement
{
    public class AdminRepository : IAdminRepository
    {
        private readonly BookManagementContext context;
        public AdminRepository() 
        { 
            this.context = new BookManagementContext();
        }
        public Admin? GetAdminByUserName(string userName)
        {
            Admin? admin = context.Admins.Where(x => x.Username.Equals(userName)).FirstOrDefault();
            return admin;
        }
    }
}
