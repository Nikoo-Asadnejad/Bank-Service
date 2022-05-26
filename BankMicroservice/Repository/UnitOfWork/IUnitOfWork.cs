using BankMicroservice.Entities;
using BankMicroservice.Repository.GenericRepository;

namespace BankMicroservice.Repository.UnitOfWork
{
  public interface IUnitOfWork 
  {
    IRepository<BankTransaction> BankTransaction();
  }
}
