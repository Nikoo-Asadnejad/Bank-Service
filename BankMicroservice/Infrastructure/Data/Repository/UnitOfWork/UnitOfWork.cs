using BankMicroservice.Entities;
using BankMicroservice.Model;
using GenericReositoryDll.Repository.GenericRepository;

namespace BankMicroservice.Repository.UnitOfWork
{
  public class UnitOfWork : IUnitOfWork
  {
    private Context _context;
    private IRepository<BankTransactionModel> _bankTransaction;
    
    public UnitOfWork(Context context)
    {
      _context = context;
    }
    public IRepository<BankTransactionModel> BankTransaction() => _bankTransaction ?? new Repository<BankTransactionModel>(_context);


  }
}
