using AutoMapper;
using FlowersShop.BL.Users.Manager;
using FlowersShop.BL.Users.Provider;
using FlowersShop.DataAccess;
using FlowersShop.DataAccess.Entities;
using FlowersShop.DataAccess.Repository;
using FlowersShopBL.Users.Provider;
using Microsoft.EntityFrameworkCore;

namespace FlowersShop.Service.IoC;

public static class ServicesConfigurator
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IRepository<UserEntity>>(x => 
            new Repository<UserEntity>(x.GetRequiredService<IDbContextFactory<FlowersShopDbContext>>()));
        services.AddScoped<IUsersProvider>(x => 
            new UsersProvider(x.GetRequiredService<IRepository<UserEntity>>(), 
                x.GetRequiredService<IMapper>()));
        services.AddScoped<IUsersManager>(x =>
            new UsersManager(x.GetRequiredService<IRepository<UserEntity>>(),
                x.GetRequiredService<IMapper>()));
    }
}