namespace BankMicroservice.Entities
{
  public class BankTransaction
  {
    public long Id { get; set; }
    public long BankId { get; set; }
    public long OrderId { get; set; }
    public long TransactionDate { get; set; }
    public string BankResult { get; set; }
  }
}
