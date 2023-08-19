namespace BankMicroservice.Dtos.Bank.Sadad
{
    public class SadadVerifyResultDto
    {
        public string ResCode { get; set; }
        public string Token { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public string RetrivalRefNo { get; set; }
        public string SystemTraceNo { get; set; }
        public string OrderId { get; set; }
        public dynamic AdditionalData { get; set; }
    }
}
