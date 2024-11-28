using Demo.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.IRepositories;

public interface IOpportunityRepository : IRepository<Opportunity>
{
    public Opportunity? Get(int id);

    public Opportunity? GetWithOrganization(int id);

    public Opportunity? GetWithApplications(int id);
}
