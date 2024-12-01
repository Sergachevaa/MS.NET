using FlowersShop.Service.IoC;
using FlowersShop.Service.Settings;

namespace FlowersShop.Service.DI;

public static class ApplicationConfigurator
{
    public static void ConfigureServices(WebApplicationBuilder builder, FlowersShopSettings FlowersShopSettings)
    {
        DbContextConfigurator.ConfigureServices(builder);
        SerilogConfigurator.ConfigureService(builder);
        SwaggerConfigurator.ConfigureServices(builder.Services);
        MapperConfigurator.ConfigureServices(builder);
        //ServicesConfigurator.ConfigureServices(builder.Services);
        ServicesConfigurator.ConfigureServices(builder.Services, FlowersShopSettings);
        AuthorizationConfigurator.ConfigureServices(builder.Services, FlowersShopSettings);
        
        builder.Services.AddControllers();
    }
    public static void ConfigureApplication(WebApplication app)
    {
        SerilogConfigurator.ConfigureApplication(app);
        SwaggerConfigurator.ConfigureApplication(app);
        DbContextConfigurator.ConfigureApplication(app);
        AuthorizationConfigurator.ConfigureApplication(app);
        
        app.UseHttpsRedirection();
        app.MapControllers();
    }
    
    
}