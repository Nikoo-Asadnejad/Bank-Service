using BankMicroservice.Dtos.BankTransaction;
using BankMicroservice.Entities;
using BankMicroservice.Persistances.ReturnTypes;
using BankMicroservice.Repository.GenericRepository;
using BankMicroservice.Repository.UnitOfWork;
using BankMicroservice.Utils;
using System.Net;

namespace BankMicroservice.Repository.BankTransactionRepository
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

    public async Task<ReturnModel<long>> GetTransactionIdByToken(string token)
    {
      ReturnModel<long> result = new();
      long transactionId =(long) _repository.GetSingleAsync(x => x.BankToken == token,x=> x.Id).Result;
      if(transactionId == null ||  transactionId == 0)
      {
        result.HttpStatusCode = HttpStatusCode.NotFound;
        result.Message = "تراکنش مورد نظر یافت نشد";
        return result;
      }

      result.HttpStatusCode = HttpStatusCode.OK;
      result.Data = transactionId;
      result.DataTitle = "Transaction Id";
      result.Message = ReturnMessage.SuccessMessage;
      return result;
    }
  }
}
