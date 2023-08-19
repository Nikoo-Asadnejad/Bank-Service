using BankMicroservice.Dtos.BankTransaction;
using BankMicroservice.Entities;
using BankMicroservice.Persistances.ReturnTypes;
using BankMicroservice.Repository.UnitOfWork;
using BankMicroservice.Utils;
using GenericReositoryDll.Repository.GenericRepository;
using HttpService.Utils;
using System.Net;

namespace BankMicroservice.Services.BankTransactions
{
  public class BankTransactionService : IBankTransactionService
  {
    private IUnitOfWork _unitOfWork;
    private IRepository<BankTransactionModel> _repository;
    public BankTransactionService(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
      _repository = _unitOfWork.BankTransaction();
    }

    public async Task<ReturnModel<long>> AddBankTransaction(AddBankTransactionInputDto inputModel)
    {

      ReturnModel<long> result = new();

      if(inputModel == null )
      {
        result.CreateInvalidInputErrorModel("Transaction Id");
        return result;
      }

      bool isExist = _repository.AnyAsync(x => x.OrderId == inputModel.OrderId).Result;
      if(isExist)
      {
        result.CreateDuplicatedErrorModel();
        return result;
      }

      BankTransactionModel bankTransaction = new(inputModel.BankId, inputModel.OrderId, inputModel.BankResult);
      
      await _repository.AddAsync(bankTransaction);

      result.CreateSuccessModel(bankTransaction.Id ,"BankTransaction Id");
      return result;
    }

    public async Task<ReturnModel<BankTransactionModel>> GetTransaction(long transactionId)
    {
      ReturnModel<BankTransactionModel> result = new();
      BankTransactionModel bankTransaction = _repository.GetSingleAsync(transactionId).Result;

      if(bankTransaction == null)
      {
        result.CreateNotFoundModel("Transaction");
        return result;
      }

      result.CreateSuccessModel(bankTransaction, "Transaction");
      return result;
    }

    public async Task<ReturnModel<BankTransactionModel>> GetTransactionByToken(string token)
    {
      ReturnModel<BankTransactionModel> result = new();
      BankTransactionModel transaction = _repository.GetSingleAsync(x => x.BankToken == token).Result;
      if(transaction == null )
      {
        result.CreateNotFoundModel("Transaction");
        return result;
      }
      
      result.CreateSuccessModel(transaction, "Transaction");
      return result;
    }

    public async Task<ReturnModel<long>> SetTransactionState(long transactionId ,bool isSuccessfull)
    {

      ReturnModel<long> result = new();

      var transaction = _repository.GetSingleAsync(transactionId).Result;
      if(transaction == null )
      {
        result.CreateNotFoundModel("Transaction Id");
        return result;
      }
      transaction.IsSuccessfull = isSuccessfull;
      await _repository.UpdateAsync(transaction);

      result.CreateSuccessModel(transaction.Id, "Transaction Id");
      return result;


    }
  }
}
