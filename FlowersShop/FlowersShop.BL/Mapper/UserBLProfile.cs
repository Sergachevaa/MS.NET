﻿using AutoMapper;
using FlowersShop.BL.Users.Entity;
using FlowersShop.DataAccess.Entities;

namespace FlowersShop.BL.Mapper;

public class UsersBLProfile : Profile
{
    public UsersBLProfile()
    {
        CreateMap<UserEntity, UserModel>()
            .ForMember(x => x.Id, y => y.MapFrom(src => src.Id))
            .ForMember(x => x.ExternalId, y => y.MapFrom(src => src.ExternalId));
        CreateMap<CreateUserModel, UserEntity>()
            .ForMember(x => x.Id, y => y.Ignore())
            .ForMember(x => x.ExternalId, y => y.Ignore())
            .ForMember(x => x.CreationTime, y => y.Ignore())
            .ForMember(x => x.ModificationTime, y => y.Ignore());
        CreateMap<UpdateUserModel, UserEntity>()
            .ForMember(x => x.Id, y => y.MapFrom(src => src.Id))
            .ForMember(x => x.ExternalId, y => y.MapFrom(src => src.ExternalId))
            .ForMember(x => x.ModificationTime, y => y.Ignore());
    }
}