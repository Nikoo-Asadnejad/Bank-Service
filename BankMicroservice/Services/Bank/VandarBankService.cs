using BankMicroservice.Dtos.Bank;
using BankMicroservice.Dtos.Payment;
using BankMicroservice.Persistances.ReturnTypes;

namespace BankMicroservice.Services.Bank
{
  public class VandarBankService : IBankService
  {
    public Task<ReturnModel<string>> GeneratePurchaseUrlAsync(string token)
    {
      throw new NotImplementedException();
    }

    public Task<ReturnModel<PaymentResultDto>> PaymentRequestAsync(PaymentInputDto paymentInput)
    {
      throw new NotImplementedException();
    }

    public Task<ReturnModel<VerifyResultDto>> VerifyRequestAsync(VerifyInputDto verifyInput)
    {
      throw new NotImplementedException();
    }
  }
}
