using System;

namespace BankMicroservice.Dtos.Bank.Sadad
{
    public class SadadPaymentDataDto
    {
        public string MerchantId { get; set; }
        public string TerminalId { get; set; }
        public int Amount { get; set; }
        public string OrderId { get; set; }
        public DateTime LocalDateTime { get; set; }
        public string SignData { get; set; }
        public string ReturnUrl { get; set; }

       // public string PurchasePage { get; set; }

        public SadadPaymentDataDto()
        {

        }
        public SadadPaymentDataDto(string key, int amount, string returnUrl, string orderId, string merchantId, string terminalId)
        {
            Amount = amount;
            OrderId = orderId;
            MerchantId = merchantId;
            SignData = key;
            ReturnUrl = returnUrl;
            TerminalId = terminalId;
            LocalDateTime = DateTime.Now;
           // PurchasePage = purchasePage;
        }
    }
}
