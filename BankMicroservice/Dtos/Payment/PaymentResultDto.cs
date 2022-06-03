namespace BankMicroservice.Dtos.Payment
{
    public class PaymentResultDto
    {
        public int ResultCode { get; set; }
        public string Token { get; set; }
        public string Description { get; set; }

        public string OrderId { get; set; }  
 
        public PaymentResultDto()
        {

        }
        public  PaymentResultDto(int resultCode ,string orderId, string token, string description)
        {
            ResultCode = resultCode;
            Token = token;
            Description = description;
            OrderId = orderId;
        }
    }
}
