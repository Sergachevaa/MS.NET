using FlowersShop.Service.IoC;

var builder = WebApplication.CreateBuilder(args);

SerilogConfigurator.ConfigureService(builder);
SwaggerConfigurator.ConfigureServices(builder.Services);
DbContextConfigurator.ConfigureService(builder);

var app = builder.Build();

SerilogConfigurator.ConfigureApplication(app);
SwaggerConfigurator.ConfigureApplication(app);
DbContextConfigurator.ConfigureApplication(app);

app.UseHttpsRedirection();
app.Run();