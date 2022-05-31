using BankMicroservice.Configurations.AppSettingItems;
using BankMicroservice.Model;
using BankMicroservice.Repository.GenericRepository;
using BankMicroservice.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using HttpService.Utils;

namespace BankMicroservice.Configuration
{
  public class Configurator
  {
    public static void InjectServices(IServiceCollection services , IConfiguration configuration)
    {
      services.AddControllers();

      //swagger
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen();

      services.AddDbContext<Context>(options => options.UseSqlServer(configuration.GetConnectionString("BankDb")));

      services.Configure<ApplicationSetting>(configuration);
      services.Configure<MeliBankData>(configuration.GetSection("MelliData"));
      services.Configure<VandarBankData>(configuration.GetSection("VandarData"));

      services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
      services.AddTransient<IUnitOfWork, UnitOfWork>();
      HttpServiceConfigurator.InjectHttpService(services);


    }
    public static void ConfigureAppPipeline(WebApplication app)
    {
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();

      app.UseAuthorization();

      app.MapControllers();

      app.Run();
    }

  }
}
