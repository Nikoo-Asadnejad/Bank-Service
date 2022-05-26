using BankMicroservice.Dtos.Bank;
using BankMicroservice.Persistances.ReturnTypes;

namespace BankMicroservice.Services
{
  public class BankService : IBankService
  {
    public Task<ReturnModel<string>> GeneratePurchaseUrlAsync(long bankId, string token)
    {
      throw new NotImplementedException();
    }

    public Task<ReturnModel<PaymentResultDto>> PaymentRequestAsync(long bankId, long orderId, int amount)
    {
      throw new NotImplementedException();
    }

    public Task<ReturnModel<VerifyResultDto>> VerifyRequestAsync(long bankId, string token)
    {
      throw new NotImplementedException();
    }
  }
}
