using Demo.Data.IRepositories;
using Demo.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data.Repositories;

public class ReviewRepository : Repository<Review>, IReviewRepository
{
    public ReviewRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public Review? Get(int id)
    {
        return _dbContext.Reviews.Find(id);
    }

    public IEnumerable<Review>? GetForOrganization(string organizationId)
    {
        return _dbContext.Reviews.FromSqlInterpolated($"EXEC GetReviewsByOrganizationId @OrganizationId = {organizationId}"); 
    }

    public IEnumerable<Review>? GetForVolunteer(string volunteerId)
    {
        return _dbContext.Reviews.FromSqlInterpolated($"EXEC GetReviewsByVolunteerId @VolunteerId = {volunteerId}");
    }

    public Review? GetWithApplication(int id)
    {
        return _dbContext.Reviews.Include(r => r.Application).FirstOrDefault(r => r.Id == id);     
    }

}
