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

    public IEnumerable<Application> GetApplicationsForOrganization(string organizationId)
    {
        return _dbContext.Applications.Join(_dbContext.Opportunities, 
            a => a.OpportunityId, 
            o => o.Id, (a,o) => new { Application = a, Opportunity = o})
            .Where(a => a.Opportunity.OrganizationId == organizationId)
            .Select(a => a.Application);
    }

    public IEnumerable<Application> GetApplicationsForVolunteer(string volunteerId)
    {
        return _dbContext.Applications.Where(a => a.VolunteerId == volunteerId);
    }

    public Application? GetApplicationWithReviews(int id)
    {
        return _dbContext.Applications.Include(a => a.Reviews).FirstOrDefault(a => a.Id == id); 
    }

    public Application? GetWithOpportunity(int id)
    {
        return _dbContext.Applications.Include(a => a.Opportunity).FirstOrDefault(a => a.Id == id);
    }

    public Application? GetWithOpportunityAndVolunteer(int id)
    {
        return _dbContext.Applications.Include(a => a.Opportunity).Include(a => a.Volunteer).ThenInclude(v => v.User).FirstOrDefault(a => a.Id == id);
    }

    public Application? GetWithVolunteerAndOrganization(int id)
    {
        return _dbContext.Applications.Include(a => a.Volunteer).Include(a => a.Opportunity).ThenInclude(o => o.Organization).FirstOrDefault(a => a.Id == id);
    }
}
