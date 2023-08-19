namespace GBankMicroservice.Dtos.Bank.Sadad
{
    public class SadadPurchaseResult
    {
        public int ResCode { get; set; }
        public string Token { get; set; }
        public long HashedCardNo { get; set; }
        public long PrimaryAccNo { get; set; }
        public long SwitchResCode { get; set; }
        public long OrderId { get; set; }
    }
}
