using BankMicroservice.Entities;
using GenericReositoryDll.Repository.GenericRepository;

namespace BankMicroservice.Repository.UnitOfWork
{
  public interface IUnitOfWork 
  {
    IRepository<BankTransactionModel> BankTransaction();
  }
}
