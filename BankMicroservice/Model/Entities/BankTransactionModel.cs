namespace BankMicroservice.Entities
{
  public class BankTransactionModel
  {
    public long Id { get; set; }
    public long BankId { get; set; }
    public long OrderId { get; set; }
    public bool IsSuccessfull { get; set; }
    public long TransactionDate { get; set; }
    public string BankToken { get; set; }
    public string BankResult { get; set; }
    
  }
}
