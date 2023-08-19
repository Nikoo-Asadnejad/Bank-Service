using System;

namespace BankMicroservice.Dtos.Payment
{
    public class VerifyResultDto
    {
        public int ResultCode { get; set; }
        public string OrderId { get; set; }
        public int Amount { get; set; }
        //for sadad
        public string RetrivalRefrenceNumber { get; set; }
        //for sadad
        public string TraceNumber { get; set; }
        public string Description { get; set; }
        public string Token { get; set; }


        public VerifyResultDto()
        {

        }
        /// <summary>
        /// For Sadad Bank
        /// </summary>
        /// <param name="resualtCode">ResultCode</param>
        /// <param name="orderId">OrderId</param>
        /// <param name="amount">Amount</param>
        /// <param name="description">Description</param>
        /// <param name="retrivalRefrenceNumber">RetrivalRefrenceNumber )</param>
        /// <param name="traceNumber">TraceNumber</param>
        /// <param name="transactionId"></param>
        public VerifyResultDto(string resultCode,string token ,string orderId,int amount,string description , string retrivalRefrenceNumber, string traceNumber)
        {
            ResultCode = Convert.ToInt32(resultCode);
            OrderId = orderId;
            Amount= amount;
            Description = description;
            RetrivalRefrenceNumber = retrivalRefrenceNumber;
            TraceNumber = traceNumber;
            Token = token;

        }

        /// <summary>
        /// For Vandar Bank
        /// </summary>
        /// <param name="status">ResultCode</param>
        /// <param name="factorNumber">OrderId</param>
        /// <param name="amount">Amount</param>
        /// <param name="description">Description</param>
        /// <param name="transId">TraceNumber</param>
        public VerifyResultDto(string status, string token, string factorNumber, int amount, string description,string errors, long transId)
        {
            ResultCode = Convert.ToInt32(status);
            OrderId = factorNumber;
            Amount = amount;
            Description = description + errors;
            TraceNumber = transId.ToString();
            Token = token;

        }
    }
}
