using Demo.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.IRepositories;

public interface IApplicationUserRepository
{
    public Task<ApplicationUser> Get(string id);
}
