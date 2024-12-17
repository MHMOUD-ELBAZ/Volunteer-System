using Demo.Data.Models;


namespace Demo.Data.IRepositories;

public interface IOrganizationRepository : IRepository<Organization>
{
    Organization? Get(string id);
    Organization? GetWithApplications(string id);
    Organization? GetWithOpportunities(string id);
}
