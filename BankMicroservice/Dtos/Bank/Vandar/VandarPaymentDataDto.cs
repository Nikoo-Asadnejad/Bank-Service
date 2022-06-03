namespace BankMicroservice.Dtos.Bank.Vandar
{
    public class VandarPaymentDataDto
    {

        public string Api_key { get; set; }
        public int Amount { get; set; }
        public string Callback_url { get; set; }

        //orderID
        public string FactorNumber { get; set; }

        public VandarPaymentDataDto()
        {
        }
        public VandarPaymentDataDto(string key, int amount, string returnUrl, string factorNumber)
        {
            Amount = amount;
            FactorNumber = factorNumber;
            Api_key = key;
            Callback_url = returnUrl;
        }
    }
}
