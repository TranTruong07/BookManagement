using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IAdminRepository
    {
        public Admin? GetAdminByUserName(string userName);
    }
}
