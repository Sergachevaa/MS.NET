using FlowersShop.Service.DI;
using FlowersShop.Service.IoC;
using FlowersShop.Service.Settings;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();
var settings = FlowerShopSettingsReader.Read(configuration);

var builder = WebApplication.CreateBuilder(args);

ApplicationConfigurator.ConfigureServices(builder, settings);

var app = builder.Build();
ApplicationConfigurator.ConfigureApplication(app);

app.Run();