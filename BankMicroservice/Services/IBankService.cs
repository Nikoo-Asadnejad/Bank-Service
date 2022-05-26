using BankMicroservice.Dtos.Bank;
using BankMicroservice.Persistances.ReturnTypes;

namespace BankMicroservice.Services
{
  public interface IBankService
  {
    Task<ReturnModel<PaymentResultDto>> PaymentRequestAsync(long bankId, long orderId, int amount);
    Task<ReturnModel<string>> GeneratePurchaseUrlAsync(long bankId, string token);
    Task<ReturnModel<VerifyResultDto>> VerifyRequestAsync(long bankId, string token);
  }
}
