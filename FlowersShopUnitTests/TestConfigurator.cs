using FlowersShop.Service.Settings;
using Microsoft.Extensions.Configuration;

namespace FlowersShopUnitTests;

public class TestConfigurator
{
    private static IConfiguration GetConfiguration()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
    }
    public static FlowersShopSettings GetSettings()
    {
        return FlowerShopSettingsReader.Read(GetConfiguration());
    }
}