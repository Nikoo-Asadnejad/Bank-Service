

namespace BankMicroservice.Configurations.AppSettingItems
{
  public class ApplicationSetting
  {
    public ConnectionString  ConnectionString { get; set; }
    public Sentry Sentry { get; set; }
    public MeliBankData MeliBankData { get; set; }
    public VandarBankData VandarBankData { get; set;}

  }
}
