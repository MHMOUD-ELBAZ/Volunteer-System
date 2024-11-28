using Demo.Data.Models;


namespace Demo.Data.IRepositories;

public interface IOrganizationRepository : IRepository<Organization>
{
    public Organization? Get(string id);
    public Organization? GetWithApplications(string id);
    public Organization? GetWithOpportunities(string id);
}
