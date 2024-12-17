using Demo.Data.Models;


namespace Demo.Data.IRepositories;
public interface IReviewRepository : IRepository<Review>
{
    Review? Get(int id);

    IEnumerable<Review>? GetForVolunteer(string volunteerId);
    IEnumerable<Review>? GetForOrganization(string organizationId);

    Review? GetWithApplication(int id);
}
