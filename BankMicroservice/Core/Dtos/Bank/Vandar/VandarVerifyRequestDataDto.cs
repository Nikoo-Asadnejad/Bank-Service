namespace BankMicroservice.Dtos.Bank.Vandar
{
    public class VandarVerifyRequestDataDto
    {
        public string Api_Key { get; set; }
        public string Token { get; set; }

        public VandarVerifyRequestDataDto()
        {

        }
        public VandarVerifyRequestDataDto(string api_key, string token)
        {
            Api_Key = api_key;
            Token = token;
        }
    }
}
