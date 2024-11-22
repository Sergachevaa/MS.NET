using AutoMapper;
using FlowersShop.BL.Users.Entity;
using FlowersShop.BL.Users.Exceptions;
using FlowersShop.BL.Users.Provider;
using FlowersShop.DataAccess.Entities;
using FlowersShop.DataAccess.Repository;

namespace FlowersShopBL.Users.Provider;

public class UsersProvider : IUsersProvider
{
    private readonly IRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;

    public UsersProvider(IRepository<UserEntity> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public IEnumerable<UserModel> GetUsers(UserFilterModel filter = null)
    {
        // Применяем фильтры
        string? namePart = filter?.NamePart;
        string? emailPart = filter?.EmailPart;
        DateTime? creationTimeFrom = filter?.CreationTimeFrom;
        DateTime? creationTimeTo = filter?.CreationTimeTo;
        DateTime? modificationTimeFrom = filter?.ModificationTimeFrom;
        DateTime? modificationTimeTo = filter?.ModificationTimeTo;
        Role? role = filter?.Role;

        var users = _userRepository.GetAll(); // Получаем всех пользователей

        // Фильтрация в памяти с помощью LINQ
        if (namePart != null)
            users = users.Where(u => (u.Name + " " + u.Surname).Contains(namePart));

        if (emailPart != null)
            users = users.Where(u => u.Email.Contains(emailPart));

        if (creationTimeFrom != null)
            users = users.Where(u => u.CreationTime >= creationTimeFrom);

        if (creationTimeTo != null)
            users = users.Where(u => u.CreationTime <= creationTimeTo);

        if (modificationTimeFrom != null)
            users = users.Where(u => u.ModificationTime >= modificationTimeFrom);

        if (modificationTimeTo != null)
            users = users.Where(u => u.ModificationTime <= modificationTimeTo);

        if (role != null)
            users = users.Where(u => u.Role == role);

        return _mapper.Map<IEnumerable<UserModel>>(users); // Маппинг в UserModel
    }

    public UserModel GetUserInfo(int id)
    {
        // Исправление имени метода с "GerUserInfo" на "GetUserInfo"
        var entity = _userRepository.GetById(id);
        if (entity == null)
        {
            throw new UserNotFoundException($"User with ID {id} not found");
        }
        return _mapper.Map<UserModel>(entity);
    }
}
