namespace FlowersShop.Service.Settings;

public class FlowerShopSettingsReader
{
    public static FlowersShopSettings Read(IConfiguration configuration)
    {
        return new FlowersShopSettings
        {
            FlowersShopDbContextConnectionString = configuration.GetValue<string>("FlowersShopDbContext"),
            ClientId = configuration.GetValue<string>("IdentityServerSettings:ClientId"),
            ClientSecret = configuration.GetValue<string>("IdentityServerSettings:ClientSecret"),
            IdentityServerUri = configuration.GetValue<string>("IdentityServerSettings:Uri")
        };
    }
}