namespace BankMicroservice.Dtos.Bank.Sadad
{
    public class SadadVerifyRequestDataDto
    {
        public string SignData { get; set; }
        public string Token { get; set; }

        public SadadVerifyRequestDataDto()
        {
        }
        public SadadVerifyRequestDataDto(string signData, string token)
        {
            SignData = signData;
            Token = token;
        }
    }
}
