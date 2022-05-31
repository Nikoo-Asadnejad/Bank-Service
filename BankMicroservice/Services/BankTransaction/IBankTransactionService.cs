using BankMicroservice.Dtos.BankTransaction;
using BankMicroservice.Persistances.ReturnTypes;

namespace BankMicroservice.Repository.BankTransactionRepository
{
  public interface IBankTransactionService
  {
    Task<ReturnModel<long>> AddBankTransaction( AddBankTransactionInputModel inputModel);
    Task<ReturnModel<long>> GetTransactionIdByToken(string token);
  }
}
