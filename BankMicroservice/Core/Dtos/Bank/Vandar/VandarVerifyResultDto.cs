namespace BankMicroservice.Dtos.Bank.Vandar
{
    public class VandarVerifyResultDto
    {
        public string Status { get; set; }
        public int Amount { get; set; }
        public int TransId { get; set; }

        //orderID
        public string FactorNumber { get; set; }
        public string Description { get; set; }
        public string Errors { get; set; }
    
    }
}
