using Demo.Data.Models;


namespace Demo.Data.IRepositories;
public interface  IApplicationRepository: IRepository<Application>
{
    public Application? GetApplication(int id);
    public Application? GetApplicationWithReviews(int id);
    public Application? GetWithOpportunityAndVolunteer(int id);
}
