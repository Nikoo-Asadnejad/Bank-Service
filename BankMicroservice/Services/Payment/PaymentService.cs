using BankMicroservice.Dtos.BankTransaction;
using BankMicroservice.Dtos.Payment;
using BankMicroservice.Persistances.ReturnTypes;
using BankMicroservice.Repository.BankTransactionRepository;

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

    public Task Verify()
    {
      throw new NotImplementedException();
    }
  }
}
