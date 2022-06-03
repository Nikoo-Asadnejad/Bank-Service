using BankMicroservice.Dtos.BankTransaction;
using BankMicroservice.Persistances.ReturnTypes;

namespace BankMicroservice.Repository.BankTransactionRepository
{
  public interface IBankTransactionService
  {
    Task<ReturnModel<long>> AddBankTransaction( AddBankTransactionInputDto inputModel);
    Task<ReturnModel<long>> GetTransactionIdByToken(string token);
    Task<ReturnModel<long>> SetTransactionState(long transactionId ,bool isSuccessfull);
  }
}
