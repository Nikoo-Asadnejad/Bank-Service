namespace BankMicroservice.Dtos.Payment
{
    public class PurchaseResultDto
    {
        public int ResultCode { get; set; }
        public string Token { get; set; }
        public long? OrderId { get; set; }
        public PurchaseResultDto()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultCode">Result code</param>
        /// <param name="token">Token</param>
        /// <param name="orderId">OrderId</param>
        public PurchaseResultDto(int resultCode, string token, long? orderId)
        {
            ResultCode = resultCode;
            Token = token;
            OrderId = orderId;
        }
    }
}
