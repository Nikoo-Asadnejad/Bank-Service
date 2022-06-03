namespace BankMicroservice.Entities
{
  public class BankTransactionModel
  {
    public long Id { get; set; }
    public int BankId { get; set; }
    public string OrderId { get; set; }
    public bool? IsSuccessfull { get; set; }
    public long TransactionDate { get; set; }
    public string BankToken { get; set; }
    public string BankResult { get; set; }
    
  }
}
