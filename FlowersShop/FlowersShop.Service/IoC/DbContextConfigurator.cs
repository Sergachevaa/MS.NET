using FlowersShop.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace FlowersShop.Service.IoC
{
    public static class DbContextConfigurator
    {
        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
                
            string connectionString = configuration.GetValue<string>("ConnectionStrings:FlowersShopDbContext");

            builder.Services.AddDbContextFactory<FlowersShopDbContext>(
                options => { options.UseNpgsql(connectionString); },
                ServiceLifetime.Scoped);
        }

        public static void ConfigureApplication(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<FlowersShopDbContext>>();
            using var context = contextFactory.CreateDbContext();
            context.Database.Migrate();
        }
        
    }
}