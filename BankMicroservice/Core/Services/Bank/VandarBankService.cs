using BankMicroservice.Configurations.AppSettingItems;
using BankMicroservice.Dtos.Bank;
using BankMicroservice.Dtos.Bank.Vandar;
using BankMicroservice.Dtos.Payment;
using BankMicroservice.Persistances.ReturnTypes;
using BankMicroservice.Services.BankTransactions;
using HttpService.Interface;
using Microsoft.Extensions.Options;
using System.Net;

namespace BankMicroservice.Services.Bank
{
  public class VandarBankService : IBankService
  {

      private readonly IHttpService _httpService;
    //  private readonly ILoggerService _loggerService;
      private readonly IBankTransactionService _bankTransactionService;
      private readonly VandarBankData _vandarBankData;

      public VandarBankService(IHttpService httpService, IOptions<VandarBankData> vandarData, 
          IBankTransactionService bankTransactionService)
      {
        _httpService = httpService;
      //  _loggerService = loggerService;
        _vandarBankData = vandarData.Value;
      _bankTransactionService = bankTransactionService;
      }
      public async Task<ReturnModel<PaymentResultDto>> PaymentRequestAsync(PaymentInputDto paymentInput)
      {
        ReturnModel<PaymentResultDto> returnValue = new();
        string paymentUrl = _vandarBankData.PaymentRequestApi;
        string returnUrl = _vandarBankData.ReturnUrl;
        string api_key = _vandarBankData.Api_Key;
        string factorNumber = paymentInput.OrderId;

        // send request to vandar
        VandarPaymentDataDto vandarPaymentData = new(api_key, paymentInput.Price, returnUrl, factorNumber);
        VandarPaymentResultDto vandarPaymentRequestResult = _httpService.PostAsync<VandarPaymentResultDto>(paymentUrl, vandarPaymentData).Result.Model;

        //map vandar result to our payment result
        PaymentResultDto paymentResult = new(vandarPaymentRequestResult.Status, paymentInput.OrderId, vandarPaymentRequestResult.Token, vandarPaymentRequestResult.Errors);

       // await _loggerService.CaptureLogAsync(LogLevel.Info, $"Payment Request has snet To Vandar Bank with result :{JsonConvert.SerializeObject(vandarPaymentRequestResult)} , " +
                                                          //    $"OrderId : {orderId}");

        returnValue.Data = paymentResult;
        return returnValue;
      }
      public async Task<ReturnModel<VerifyResultDto>> VerifyRequestAsync(VerifyInputDto verifyInput)
      {
      ReturnModel<VerifyResultDto> returnValue = new();
        string verifyUrl = _vandarBankData.VerifyApi;
        string api_key = _vandarBankData.Api_Key;

        //send request to vandar
        VandarVerifyRequestDataDto vandarVerifyRequestData = new(api_key, verifyInput.Token);
        VandarVerifyResultDto vandarVerifyResult = _httpService.PostAsync<VandarVerifyResultDto>(verifyUrl, vandarVerifyRequestData).Result.Model;

        // map vandar result to our verify result // Factor Number is our OrderId
        VerifyResultDto verifyResult = new(vandarVerifyResult.Status, verifyInput.Token, vandarVerifyResult.FactorNumber,
                                             vandarVerifyResult.Amount, vandarVerifyResult.Description, vandarVerifyResult.Errors,
                                              vandarVerifyResult.TransId);

       // await _loggerService.CaptureLogAsync(LogLevel.Info, $"Verify Request has sent To Vandar Bank with result : {JsonConvert.SerializeObject(vandarVerifyResult)} ," +
       //                                                        $" Token : {token} , Api_Key : {api_key}");

        returnValue.Data = verifyResult;
        return returnValue;
      }
      public async Task<ReturnModel<string>> GeneratePurchaseUrlAsync(string token)
      {
      ReturnModel<string> result = new();
        string purchaseUrl;
        purchaseUrl = _vandarBankData.PurchaseApi + $"/{token}";
        result.Data = purchaseUrl;
        return result;
      }
      public async Task<ReturnModel<string>> GenerateResultUrlAsync(string token, long? transactionId)
      {
        try
        {
        ReturnModel<string> result = new();
          string resultUrl;
          long bankTransactionId;
          if (transactionId != null)
          {
            bankTransactionId = (long)transactionId;
          }
          else
          {
            var getTransactionByToken = _bankTransactionService.GetTransactionByToken(token).Result;
            bankTransactionId = getTransactionByToken.Data.Id;
          }
          result.HttpStatusCode = HttpStatusCode.OK;
          result.Message = ReturnMessage.SuccessMessage;
          result.DataTitle = "CheckOut Url";
          resultUrl = _vandarBankData.ResultUrl + $"?TransactionId={bankTransactionId}";
          result.Data = resultUrl;
          return result;


        }
        catch (Exception ex)
        {
         // await _loggerService.CaptureLogAsync(LogLevel.Error, ex, "An Error Has Ocuured in GenerateResultUrlAsync method / SadadBankService ");
          throw ex;
        }



      }

    



  }
}
