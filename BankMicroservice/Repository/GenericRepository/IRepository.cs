namespace BankMicroservice.Repository
{
  public interface IRepository
  {
    Task<T> GetById<T>(long id);
    Task<T> GetList<T>();
    Task<T> Add<T>(T model);
  }
}
