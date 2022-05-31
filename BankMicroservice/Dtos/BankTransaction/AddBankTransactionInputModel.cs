namespace BankMicroservice.Dtos.BankTransaction
{
  public class AddBankTransactionInputModel
  {
    public long BankId { get; set; }
    public long OrderId { get; set; }
    public long TransactionDate { get; set; }
    public object BankResult { get; set; }
  }
}
