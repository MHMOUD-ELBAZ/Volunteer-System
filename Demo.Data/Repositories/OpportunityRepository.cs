using Demo.Data.IRepositories;
using Demo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace Demo.Data.Repositories;

public class OpportunityRepository: Repository<Opportunity>, IOpportunityRepository
{
    public OpportunityRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public Opportunity? Get(int id)
    {
        return _dbContext.Opportunities.Include(o => o.OpportunitySkills).SingleOrDefault(o => o.Id == id);
    }

    public Opportunity? GetWithApplications(int id)
    {
        return _dbContext.Opportunities.Include(o =>  o.Applications).FirstOrDefault(o => o.Id == id);
    }

    public Opportunity? GetWithOrganization(int id)
    {
        return _dbContext.Opportunities.Include(o => o.Organization).Include(o => o.OpportunitySkills).FirstOrDefault(o => o.Id == id);
    }
}
