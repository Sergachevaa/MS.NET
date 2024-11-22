using FlowersShop.Service.DI;
using FlowersShop.Service.IoC;

var builder = WebApplication.CreateBuilder(args);

ApplicationConfigurator.ConfigureServices(builder);

var app = builder.Build();
ApplicationConfigurator.ConfigureApplication(app);

app.Run();