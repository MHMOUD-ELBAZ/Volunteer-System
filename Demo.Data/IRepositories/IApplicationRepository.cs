using Demo.Data.Models;


namespace Demo.Data.IRepositories;
public interface  IApplicationRepository: IRepository<Application>
{
    IEnumerable<Application> GetApplicationsForVolunteer(string volunteerId);
    IEnumerable<Application> GetApplicationsForOrganization(string organizationId);
    public Application? GetApplication(int id);
    public Application? GetApplicationWithReviews(int id);
    public Application? GetWithOpportunityAndVolunteer(int id);
    public Application? GetWithVolunteerAndOrganization(int id);
    public Application? GetWithOpportunity(int id);
}
