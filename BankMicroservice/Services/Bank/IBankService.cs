using BankMicroservice.Dtos.Bank;
using BankMicroservice.Dtos.Payment;
using BankMicroservice.Persistances.ReturnTypes;

namespace BankMicroservice.Services
{
  public interface IBankService
  {
    Task<ReturnModel<PaymentResultDto>> PaymentRequestAsync(PaymentInputDto paymentInput);
    Task<ReturnModel<string>> GeneratePurchaseUrlAsync(string token);
    Task<ReturnModel<VerifyResultDto>> VerifyRequestAsync(VerifyInputDto verifyInput);
  }
}
