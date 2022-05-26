using BankMicroservice.Model;
using BankMicroservice.Persistance.Enumerations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BankMicroservice.Repository.GenericRepository
{
  public class Repository<T> : IRepository<T> where T : class
  {

    private readonly Context _context;
    private readonly DbSet<T> _model;
    public Repository(Context context)
    {
      _context = context;
      _model = _context.Set<T>();
    }
    public async Task AddAsync(T model)
    {
      await _model.AddAsync(model);
      await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<T> models)
    {
      await _model.AddRangeAsync(models);
      await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
      T model =  _context.FindAsync<T>(id).Result;
      if(model != null) _model.Remove(model);
      await _context.SaveChangesAsync();
    }

    public async Task DeleteRangeAsync(IEnumerable<T> models)
    {
      _model.RemoveRange(models);
      await _context.SaveChangesAsync();
    }

    public async Task<long> GetCountAsync(Expression<Func<T, bool>> query = null)
    => await _model.CountAsync(query);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="query">Get where query</param>
    /// <param name="orderBy">Order by orderBy expression</param>
    /// <param name="orderByType">Desc | Asc </param>
    /// <param name="skip">skip</param>
    /// <param name="take">take</param>
    /// <returns></returns>
    public async Task<IQueryable<T>> GetListAsync(Expression<Func<T, bool>> query = null,
      Expression<Func<T, IOrderedQueryable<T>>> orderBy = null,
      OrderByType? orderByType = null,
      int? skip = null,
      int? take = null,
      bool? distinct = null)
    {

      var models =  _model.AsQueryable().AsNoTrackingWithIdentityResolution();
      if (query != null) models = models.Where(query);
      if(orderBy != null && orderByType == OrderByType.Asc) models = models.OrderBy(orderBy);
      if(orderBy != null && orderByType == OrderByType.Desc) models = models.OrderByDescending(orderBy);
      if(skip != null) models.Skip((int)skip);
      if(take != null) models.Take((int)take);
      if(distinct != null) models.Distinct();

      
      return models;

    }

    public async Task<T> GetSingleAsync(long id)
    => await _model.FindAsync(id);

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> query)
    => await _model.FindAsync(query);

    public async Task UpdateAsync(T model)
    {
      _model.Update(model);
      await _context.SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(IEnumerable<T> models)
    {
      _model.UpdateRange(models);
      await _context.SaveChangesAsync();
    }
  }
}
