using AutoMapper;
using FlowersShop.BL.Users.Entity;
using FlowersShop.Service.Controllers.Entities.UserEntities;

namespace FlowersShop.Service.Mapper;

public class UsersServiceProfile : Profile
{
    public UsersServiceProfile()
    {
        CreateMap<UserFilter, UserFilterModel>();
        CreateMap<RegisterUserRequest, CreateUserModel>();
    }
}