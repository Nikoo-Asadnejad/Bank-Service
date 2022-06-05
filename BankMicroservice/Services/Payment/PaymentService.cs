using BankMicroservice.Dtos.BankTransaction;
using BankMicroservice.Dtos.Payment;
using BankMicroservice.Persistances.ReturnTypes;
using BankMicroservice.Services.BankTransactions;
using System.Net;

namespace BankMicroservice.Services.Payment
{
  public class PaymentService : IPaymentService
  {
    private readonly Func<int, IBankService> _bankService;
    private readonly IBankTransactionService _bankTransactionService;

    public PaymentService(Func<int, IBankService> bankService  , IBankTransactionService bankTransactionService)
    {
      _bankService = bankService;
      _bankTransactionService = bankTransactionService;
    }
    public async Task<ReturnModel<PaymentResultDto>> Payment(PaymentInputDto paymentInput)
    {
      var bankService = _bankService(paymentInput.BankId);
      
      ReturnModel<PaymentResultDto> paymentRequestResult = bankService.PaymentRequestAsync(paymentInput).Result;
      AddBankTransactionInputDto addBankTransactionInput = new(paymentInput.BankId, paymentInput.OrderId, paymentRequestResult);
      ReturnModel<long> addBankTransactionResult = _bankTransactionService.AddBankTransaction(addBankTransactionInput).Result;

      return paymentRequestResult;
    }

    public async Task<ReturnModel<VerifyResultDto>> Verify(VerifyInputDto verifyInput)
    {
      ReturnModel<VerifyResultDto> result = new();
      var bankService = _bankService(verifyInput.BankId);
      var verifyRequestResult = bankService.VerifyRequestAsync(verifyInput).Result;

      long transactionId = _bankTransactionService.GetTransactionByToken(verifyRequestResult.Data.Token).Result.Data.Id;

      if(verifyRequestResult.Data.ResultCode == 0)
      {
        await _bankTransactionService.SetTransactionState(transactionId, isSuccessfull : true);
      }
      else
      {
        await _bankTransactionService.SetTransactionState(transactionId, isSuccessfull: false);
      }


      result.CreateSuccessModel(verifyRequestResult.Data, "Verify Result");
      return result;
    }
  }
}
