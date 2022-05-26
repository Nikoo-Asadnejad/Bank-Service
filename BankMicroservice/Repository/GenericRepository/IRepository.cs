using BankMicroservice.Persistance.Enumerations;
using System.Linq.Expressions;

namespace BankMicroservice.Repository.GenericRepository
{
  public interface IRepository<T>  where T : class
  {
    Task<T> GetSingleAsync(long id);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> query);
    Task<IQueryable<T>> GetListAsync(Expression<Func<T, bool>> query = null,
                                Expression<Func<T, IOrderedQueryable<T>>> orderBy = null,
                                OrderByType? orderByType = null,
                                int? skip = 0,
                                int? take = null,
                                bool? distinct = null);
    Task<long> GetCountAsync(Expression<Func<T, bool>> query = null);

    Task AddAsync(T model);
    Task AddRangeAsync(IEnumerable<T> models);
    Task UpdateAsync(T model);
    Task UpdateRangeAsync(IEnumerable<T> models);
    Task DeleteAsync(long id);
    Task DeleteRangeAsync(IEnumerable<T> models);



  }
}
