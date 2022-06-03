using BankMicroservice.Dtos.BankTransaction;
using BankMicroservice.Entities;
using BankMicroservice.Persistances.ReturnTypes;
using BankMicroservice.Repository.GenericRepository;
using BankMicroservice.Repository.UnitOfWork;
using BankMicroservice.Utils;
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

      bool isExist = _repository.AnyAsync(x => x.OrderId == inputModel.OrderId).Result;
      if(isExist)
      {
        result.HttpStatusCode = HttpStatusCode.BadRequest;
        result.Message = "شماره سفارش تکراری است";
        return result;
      }

      BankTransactionModel bankTransaction = new BankTransactionModel()
      {
        BankId = inputModel.BankId,
        OrderId = inputModel.OrderId,
        IsSuccessfull = null,
        BankResult = inputModel.BankResult.Serialize<object>(),
        TransactionDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
        
      };
      await _repository.AddAsync(bankTransaction);

      result.HttpStatusCode = HttpStatusCode.OK;
      result.Data = bankTransaction.Id;
      result.Message = ReturnMessage.SuccessMessage;
      result.DataTitle = "BankTransaction Id";

      return result;
    }

    public async Task<ReturnModel<BankTransactionModel>> GetTransaction(long transactionId)
    {
      ReturnModel<BankTransactionModel> result = new();

      BankTransactionModel bankTransaction = _repository.GetSingleAsync(transactionId).Result;


      result.HttpStatusCode = HttpStatusCode.OK;
      result.Data = bankTransaction;
      result.DataTitle = "Transaction";
      result.Message = ReturnMessage.SuccessMessage;
      return result;
    }

    public async Task<ReturnModel<BankTransactionModel>> GetTransactionByToken(string token)
    {
      ReturnModel<BankTransactionModel> result = new();
      BankTransactionModel transaction = _repository.GetSingleAsync(x => x.BankToken == token).Result;
      if(transaction == null )
      {
        result.HttpStatusCode = HttpStatusCode.NotFound;
        result.Message = "تراکنش مورد نظر یافت نشد";
        return result;
      }

      result.HttpStatusCode = HttpStatusCode.OK;
      result.Data = transaction;
      result.DataTitle = "Transaction Id";
      result.Message = ReturnMessage.SuccessMessage;
      return result;
    }

    public async Task<ReturnModel<long>> SetTransactionState(long transactionId ,bool isSuccessfull)
    {

      ReturnModel<long> result = new();

      var transaction = _repository.GetSingleAsync(transactionId).Result;
      transaction.IsSuccessfull = isSuccessfull;
      await _repository.UpdateAsync(transaction);

      result.HttpStatusCode = HttpStatusCode.OK;
      result.Data = transaction.Id;
      result.DataTitle = "Transaction Id";
      result.Message = ReturnMessage.SuccessMessage;
      return result;


    }
  }
}
