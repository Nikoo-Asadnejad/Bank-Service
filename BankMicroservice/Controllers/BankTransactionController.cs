using BankMicroservice.Entities;
using BankMicroservice.Persistances.ReturnTypes;
using BankMicroservice.Services.BankTransactions;
using Microsoft.AspNetCore.Mvc;

namespace BankMicroservice.Controllers
{
  [Controller]
  public class BankTransactionController : Controller
  {
    private readonly IBankTransactionService _bankTransactionService;
    public BankTransactionController(IBankTransactionService bankTransactionService)
    {
      _bankTransactionService = bankTransactionService;
    }

    /// <summary>
    /// Gets a bank transaction by id
    /// </summary>
    /// <param name="transactionId"></param>
    /// <returns></returns>
    [HttpGet("Api/BankTransactions/{transactionId}")]
    public async Task<IActionResult> GetTransaction([FromRoute] long transactionId)
    {
      ReturnModel<BankTransactionModel> result = _bankTransactionService.GetTransaction(transactionId).Result;
      return StatusCode((int)result.HttpStatusCode, result);
    }


  }
}
