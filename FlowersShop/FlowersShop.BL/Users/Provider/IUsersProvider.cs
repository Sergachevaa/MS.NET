using FlowersShop.BL.Users.Entity;

namespace FlowersShop.BL.Users.Provider;

public interface IUsersProvider
{
    IEnumerable<UserModel> GetUsers(UserFilterModel filter = null);
    UserModel GetUserInfo(int id);
}