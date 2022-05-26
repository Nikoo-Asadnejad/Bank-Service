namespace GrootFinancial.Dtos.BankTransaction
{
    public class PaymentResultDto
    {
        public int ResultCode { get; set; }
        public string Token { get; set; }
        public string Description { get; set; }

        public long? OrderId { get; set; }  
 
        public PaymentResultDto()
        {

        }
        public  PaymentResultDto(int resultCode ,long? orderId, string token, string description)
        {
            ResultCode = resultCode;
            Token = token;
            Description = description;
            OrderId = orderId;
        }
    }
}
