using Demo.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private protected readonly AppDbContext _dbContext;

    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(T entity)
    {
        _dbContext.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public IEnumerable<T> GetAll() => _dbContext.Set<T>().AsNoTracking();

    public void Update(T entity) => _dbContext.Set<T>().Update(entity);

    public int Save() => _dbContext.SaveChanges();

}
