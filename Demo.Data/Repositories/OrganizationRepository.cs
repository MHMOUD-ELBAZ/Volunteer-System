using Demo.Data.IRepositories;
using Demo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Repositories;


public class OrganizationRepository : Repository<Organization>, IOrganizationRepository
{
    public OrganizationRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public Organization? Get(string id)
    {
        return _dbContext.Organizations.Include(o => o.User).FirstOrDefault(o => o.OrganizationId == id);
    }

    public Organization? GetWithApplications(string id)
    {
        return _dbContext.Organizations.Include(o => o.Applications).FirstOrDefault(o => o.OrganizationId == id);  
    }

    public Organization? GetWithOpportunities(string id)
    {
        return _dbContext.Organizations.Include(o => o.Opportunities).FirstOrDefault(o => o.OrganizationId == id);
    }
}
