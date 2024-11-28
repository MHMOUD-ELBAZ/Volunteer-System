using Demo.Data.IRepositories;
using Demo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Repositories;

public class  ApplicationRepository : Repository<Application>, IApplicationRepository
{
    public ApplicationRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public Application? GetApplication(int id)
    {
        return _dbContext.Applications.Find(id);
    }

    public Application? GetApplicationWithReviews(int id)
    {
        return _dbContext.Applications.Include(a => a.Reviews).FirstOrDefault(a => a.Id == id); 
    }

    public Application? GetWithOpportunityAndVolunteer(int id)
    {
        return _dbContext.Applications.Include(a => a.Opportunity).Include(a => a.Volunteer).FirstOrDefault(a => a.Id == id);
    }
}
