using BankMicroservice.Configurations.AppSettingItems;
using BankMicroservice.Model;
using BankMicroservice.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using HttpService.Utils;
using BankMicroservice.Services.Payment;
using BankMicroservice.Services;
using BankMicroservice.Persistances.Enumerations;
using BankMicroservice.Services.Bank;
using HttpService.Configuration;
using Microsoft.OpenApi.Models;
using System.Reflection;
using BankMicroservice.Services.BankTransactions;
using GenericRepositoryDll.Configuration;
using ErrorHandlingDll.Configurations;

namespace BankMicroservice.Configuration
{
  public class Configurator
  {
    public static void InjectServices(IServiceCollection services , IConfiguration configuration)
    {
      services.AddControllers();

      //swagger
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen(options =>
      {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
          Version = "v1",
          Title = "Banks API",
          Description = "An ASP.NET Core Web API for Interacting with different Bank Gateways",
         
        });

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

      });

      services.AddDbContext<Context>(options => options.UseSqlServer(configuration.GetConnectionString("BankDb")));

      services.Configure<ApplicationSetting>(configuration);
      services.Configure<MeliBankData>(configuration.GetSection("MelliData"));
      services.Configure<VandarBankData>(configuration.GetSection("VandarData"));
      
      HttpServiceConfigurator.InjectHttpService(services);
      GenericRepositoryConfigurator.InjectServices(services);
      ErrorHandlingDllConfigurator.InjectServices(services, configuration);
     
      services.AddScoped<DbContext, Context>();
      //services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
      services.AddTransient<IUnitOfWork, UnitOfWork>();
      services.AddTransient<IBankTransactionService, BankTransactionService>();
      services.AddTransient<IPaymentService, PaymentService>();
      services.AddTransient<SadadBankService>();
      services.AddTransient<VandarBankService>();
      services.AddTransient<Func<int, IBankService>>(serviceProvider => bankId =>
      {
          switch(bankId)
        {
          case (int)BankIds.Sadad : return serviceProvider.GetService<SadadBankService>();
          case (int)BankIds.Vandar : return serviceProvider.GetService<VandarBankService>();
          default : return  serviceProvider.GetService<SadadBankService>();
        }
          
      });
      


    }
    public static void ConfigureAppPipeline(WebApplication app)
    {
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI(c => {
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "Banks API");
        });
      }

      ErrorHandlingDllConfigurator.ConfigureAppPipeline(app);
      app.UseHttpsRedirection();

      app.UseAuthorization();

      app.MapControllers();

      app.Run();
    }

  }
}
