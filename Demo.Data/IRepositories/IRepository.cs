using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.IRepositories;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();

    void Delete(T entity);

    void Update(T entity);

    void Add(T entity);

    public int Save();
}
