using BankMicroservice.Dtos.BankTransaction;
using BankMicroservice.Persistances.ReturnTypes;

namespace BankMicroservice.Repository.BankTransactionRepository
{
  public interface IBankTransactionRepository
  {
    Task<ReturnModel<long>> AddBankTransaction( AddBankTransactionInputModel inputModel);

  }
}
