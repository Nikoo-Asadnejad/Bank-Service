using BankMicroservice.Utils;

namespace BankMicroservice.Dtos.BankTransaction
{
  public class AddBankTransactionInputDto
  {
    public int BankId { get; set; }
    public string OrderId { get; set; }
    public long TransactionDate { get; set; }
    public string BankResult { get; set; }

    public AddBankTransactionInputDto()
    {

    }
    public AddBankTransactionInputDto(int bankId , string orderId , object bankResult)
    {
      BankId = bankId;
      OrderId = orderId;
      TransactionDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
      BankResult = bankResult.Serialize();

    }

  }
}
