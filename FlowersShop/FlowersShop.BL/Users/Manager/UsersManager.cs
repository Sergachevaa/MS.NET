using AutoMapper;
using FlowersShop.BL.Users.Entity;
using FlowersShop.BL.Users.Exceptions;
using FlowersShop.DataAccess.Entities;
using FlowersShop.DataAccess.Repository;

namespace FlowersShop.BL.Users.Manager;

public class UsersManager : IUsersManager
{
    private readonly IRepository<UserEntity> _usersRepository;
    private readonly IMapper _mapper;
    public UsersManager(IRepository<UserEntity> usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }
    public UserModel CreateUser(CreateUserModel createModel)
    {
        var entity = _mapper.Map<UserEntity>(createModel);
        entity = _usersRepository.Save(entity);
        return _mapper.Map<UserModel>(entity);
    }
    public void DeleteUser(int id)
    {
        try
        {
            var entity = _usersRepository.GetById(id);
            _usersRepository.Delete(entity);
        }
        catch (Exception e)
        {
            throw new UserNotFoundException(e.Message);
        }
    }
    public UserModel UpdateUser(UpdateUserModel updateModel)
    {
        var entity = _mapper.Map<UserEntity>(updateModel);
        entity = _usersRepository.Save(entity);
        return _mapper.Map<UserModel>(entity);
    }
}