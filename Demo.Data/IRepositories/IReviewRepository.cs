using Demo.Data.Models;


namespace Demo.Data.IRepositories;
public interface IReviewRepository : IRepository<Review>
{
    Review? Get(int id);

    Review? GetWithAllData(int id);

    Review? GetWithApplication(int id);
}
