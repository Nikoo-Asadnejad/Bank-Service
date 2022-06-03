using BankMicroservice.Dtos.Payment;
using BankMicroservice.Services.Payment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace BankMicroservice.Controllers
{
  [Controller]
  public class BankController : Controller
  {
    private readonly IPaymentService _paymentService;
    public BankController(IPaymentService paymentService)
    {
      _paymentService = paymentService;
    }

    /// <summary>
    /// Sends A Payment Request to Bank user has sent its id
    /// </summary>
    /// <param name="paymentInput"></param>
    /// <returns></returns>
    [HttpPost("Api/Banks/PaymentRequest")]
    public async Task<IActionResult> PaymentRequest(PaymentInputDto paymentInput)
    {
      var paymentResult = _paymentService.Payment(paymentInput).Result;
      return StatusCode((int)paymentResult.HttpStatusCode, paymentResult);
    }


  }
}
