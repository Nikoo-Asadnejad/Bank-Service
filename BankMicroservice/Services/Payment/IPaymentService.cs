using BankMicroservice.Dtos.Payment;
using BankMicroservice.Persistances.ReturnTypes;

namespace BankMicroservice.Services.Payment
{
  public interface IPaymentService
  {
    Task<ReturnModel<PaymentResultDto>> Payment(PaymentInputDto paymentInput);
    Task<ReturnModel<VerifyResultDto>> Verify(VerifyInputDto verifyInput);
  }
}
