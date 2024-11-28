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

    public Review? GetWithApplication(int id)
    {
        return _dbContext.Reviews.Include(r => r.Application).FirstOrDefault(r => r.Id == id);     
    }

    public Review? GetWithAllData(int id)
    {
        return _dbContext.Reviews.Include(r => r.Application).Include(r => r.Organization).Include(r=>r.Volunteer).FirstOrDefault(r => r.Id == id);
    }
}
