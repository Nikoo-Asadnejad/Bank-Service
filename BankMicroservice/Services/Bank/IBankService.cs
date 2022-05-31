using BankMicroservice.Dtos.Bank;
using BankMicroservice.Persistances.ReturnTypes;

namespace BankMicroservice.Services
{
  public interface IBankService
  {
    Task<ReturnModel<PaymentResultDto>> PaymentRequestAsync(string orderId, int amount);
    Task<ReturnModel<string>> GeneratePurchaseUrlAsync(string token);
    Task<ReturnModel<VerifyResultDto>> VerifyRequestAsync(string token);
  }
}
