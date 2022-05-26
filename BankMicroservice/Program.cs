using BankMicroservice.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Configurator.InjectServices(builder.Services,builder.Configuration);

var app = builder.Build();
// Configure the HTTP request pipeline.
Configurator.ConfigureAppPipeline(app);
