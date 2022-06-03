using BankMicroservice.Dtos.BankTransaction;
using BankMicroservice.Entities;
using BankMicroservice.Persistances.ReturnTypes;

namespace BankMicroservice.Services.BankTransactions
{
  public interface IBankTransactionService
  {
    Task<ReturnModel<long>> AddBankTransaction( AddBankTransactionInputDto inputModel);
    Task<ReturnModel<BankTransactionModel>> GetTransactionByToken(string token);

    Task<ReturnModel<BankTransactionModel>> GetTransaction(long transactionId);
    Task<ReturnModel<long>> SetTransactionState(long transactionId ,bool isSuccessfull);
  }
}
