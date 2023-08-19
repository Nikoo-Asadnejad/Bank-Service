using BankMicroservice.Configurations.AppSettingItems;
using BankMicroservice.Dtos.Bank;
using BankMicroservice.Dtos.Bank.Sadad;
using BankMicroservice.Dtos.Payment;
using BankMicroservice.Entities;
using BankMicroservice.Persistances.ReturnTypes;
using BankMicroservice.Services.BankTransactions;
using HttpService.Interface;
using Microsoft.Extensions.Options;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace BankMicroservice.Services
{
  public class SadadBankService : IBankService
  {
    private readonly IHttpService _httpService;
    private readonly IBankTransactionService _bankTransactionService;
    private readonly MeliBankData _meliBankData;
    private readonly string _paymentApi;
    private readonly string _verifyApi;
    private readonly string _purchaseUrl;
    private readonly string _merchantId;
    private readonly string _terminalId;
    private readonly string _returnURl;
    public SadadBankService(IHttpService httpService, IOptions<MeliBankData> meliData,
        IBankTransactionService bankTransactionService)
    {
      _httpService = httpService;
     // _loggerService = loggerService;
      _meliBankData = meliData.Value;
      _bankTransactionService = bankTransactionService;
      _paymentApi = _meliBankData.PaymentRequestApi;
      _purchaseUrl = _meliBankData.PurchaseUrl;
      _verifyApi = _meliBankData.VerifyApi;
      _merchantId = _meliBankData.MerchantId;
      _terminalId = _meliBankData.TerminalId;
      _returnURl = _meliBankData.ReturnUrl;


    }

    public async Task<ReturnModel<string>> GeneratePurchaseUrlAsync(string token)
    {

      try
      {
        ReturnModel<string> returnValue = new();
        string purchaseUrl = _purchaseUrl + $"?token={token}";
        returnValue.CreateSuccessModel(purchaseUrl, "Purchase url");
        return returnValue;
      }
      catch (Exception ex)
      {
       // await _loggerService.CaptureLogAsync(LogLevel.Error, ex, "An Error Has Ocuured in PurchaseAsync method / SadadBankService ");
        throw ex;
      }


    }

    public async Task<ReturnModel<PaymentResultDto>> PaymentRequestAsync(PaymentInputDto paymentInput)
    {
      try
      {

        ReturnModel<PaymentResultDto> returnValue = new();
      
        string signData = await CreateSadadKeyAsync(paymentInput.OrderId, paymentInput.Price);

        //send request to sadad bank 
        SadadPaymentDataDto sadadPaymentData = new(signData, paymentInput.Price, _returnURl, paymentInput.OrderId, _merchantId, _terminalId);
        SadadPaymentResultDto sadadPaymentRequestResult = _httpService.PostAsync<SadadPaymentResultDto>(_paymentApi, sadadPaymentData).Result.Model;
        if (sadadPaymentRequestResult == null)
        {
          returnValue.HttpStatusCode = HttpStatusCode.BadRequest;
          returnValue.Message = "پاسخی از بانک دریافت نشد";
        //  await _loggerService.CaptureLogAsync(LogLevel.Error, "The BankResult For Payment Request to Sadad Was null");
          return returnValue;
        }
        //map sadad result to our payment result
        PaymentResultDto paymentResult = new(sadadPaymentRequestResult.ResCode, paymentInput.OrderId, sadadPaymentRequestResult.Token, sadadPaymentRequestResult.Description);

      //  await _loggerService.CaptureLogAsync(LogLevel.Info, $"Payment Request has sent To Sadad Bank the result is :{JsonConvert.SerializeObject(sadadPaymentRequestResult)} ," +
           //                                                    $"OrderId : {orderId}");

        returnValue.CreateSuccessModel(paymentResult, "Payment request Result");
        return returnValue;

      }
      catch (Exception ex)
      {
     //   await _loggerService.CaptureLogAsync(LogLevel.Error, ex, $"An Error Has Ocuured in PaymentRequestAsync method / SadadBankService error");
        throw ex;
      }


    }

    public async Task<ReturnModel<VerifyResultDto>> VerifyRequestAsync(VerifyInputDto verifyInput)
    {
      try
      {
        ReturnModel<VerifyResultDto> returnValue = new();
        string signData = await DecodeSadadKeyAsync(verifyInput.Token);

        //send request to sadad
        SadadVerifyRequestDataDto sadadVerifyRequestData = new(signData, verifyInput.Token);

        //SadadVerifyResultDto
        SadadVerifyResultDto sadadVerifyResult = _httpService.PostAsync<SadadVerifyResultDto>(_verifyApi, sadadVerifyRequestData).Result.Model;
        if (sadadVerifyRequestData == null)
        {
          returnValue.CreateBadRequestModel("Verify Result","پاسخی از بانک دریافت نشد");
         // await _loggerService.CaptureLogAsync(LogLevel.Error, "The BankResult For verify Request to Sadad Was null");
          return returnValue;
        }
        // map sadad result to our verify result
        VerifyResultDto verifyResult = new(sadadVerifyResult.ResCode, verifyInput.Token, sadadVerifyResult.OrderId,
                                             Convert.ToInt32(sadadVerifyResult.Amount), sadadVerifyResult.Description,
                                             sadadVerifyResult.RetrivalRefNo, sadadVerifyResult.SystemTraceNo);


      //  await _loggerService.CaptureLogAsync(LogLevel.Info, $"Verify Request has sent To Sadad Bank with result : {JsonConvert.SerializeObject(sadadVerifyResult)} ," +
                                                        //    $" token : {token} , signData : {signData}");
        returnValue.CreateSuccessModel(verifyResult, "Verify Result");
        return returnValue;

      }
      catch (Exception ex)
      {
      //  await _loggerService.CaptureLogAsync(LogLevel.Error, ex, "An Error Has Ocuured in VerifyRequestAsync method / SadadBankService ");
        throw ex;
      }

    }

    public async Task<ReturnModel<string>> GenerateResultUrlAsync(string token, long transactionId)
    {
      try
      {
        ReturnModel<string> result = new();
        string resultUrl;
        long bankTransactionId;
        if (transactionId != null)
        {
          bankTransactionId = transactionId;
        }
        else
        {
          ReturnModel<BankTransactionModel> transaction = _bankTransactionService.GetTransactionByToken(token).Result;
          bankTransactionId = transaction.Data.Id;
        }

        resultUrl = _meliBankData.ResultUrl + $"?transactionId={bankTransactionId}";
        result.CreateSuccessModel(resultUrl, "CheckOut Url");
        return result;


      }
      catch (Exception ex)
      {
       // await _loggerService.CaptureLogAsync(LogLevel.Error, ex, "An Error Has Ocuured in GenerateResultUrlAsync method / SadadBankService ");
        throw ex;
      }

    }

    #region SignData
    // hash sadad key (sign data)
    public async Task<string> CreateSadadKeyAsync(string orderId, int price)
    {
      try
      {
        string merchantId = _meliBankData.MerchantId;
        string terminalId = _meliBankData.TerminalId;
        string merchantKey = _meliBankData.MerchantKey;
        string signData;

        byte[] dataBytes = Encoding.UTF8.GetBytes(string.Format("{0};{1};{2}", terminalId, orderId, price));
        SymmetricAlgorithm symmetric = SymmetricAlgorithm.Create("TripleDes");
        symmetric.Mode = CipherMode.ECB;
        symmetric.Padding = PaddingMode.PKCS7;
        ICryptoTransform encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(merchantKey), new byte[8]);
        signData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));

        return signData;

      }
      catch (Exception ex)
      {
       // await _loggerService.CaptureLogAsync(LogLevel.Error, ex, "An Error Has Ocuured in CreateSadadKeyAsync method / SadadBankService ");
        throw ex;
      }


    }

    public async Task<string> DecodeSadadKeyAsync(string token)
    {

      try
      {
        string merchantKey = _meliBankData.MerchantKey;
        byte[] dataBytes = Encoding.UTF8.GetBytes(token);
        SymmetricAlgorithm symmetric = SymmetricAlgorithm.Create("TripleDes");
        symmetric.Mode = CipherMode.ECB;
        symmetric.Padding = PaddingMode.PKCS7;
        ICryptoTransform encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(merchantKey), new byte[8]);
        string signedData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));
        return signedData;
      }
      catch (Exception ex)
      {
      //  await _loggerService.CaptureLogAsync(LogLevel.Error, ex, "An Error Has Ocuured in DecodeSadadKeyAsync method / SadadBankService ");
        throw ex;

      }


    }




    #endregion
  }
}
