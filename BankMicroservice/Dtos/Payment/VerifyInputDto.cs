namespace BankMicroservice.Dtos.Payment
{
  public class VerifyInputDto
  {
    public int BankId { get; set; }
    public string Token { get; set; }
  }
}
