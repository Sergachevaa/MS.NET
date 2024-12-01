using FlowersShop.BL.Authorization.Entities;
using FlowersShop.BL.Users.Entity;

namespace FlowersShop.BL.Authorization;

public interface IAuthProvider
{
    Task<UserModel> RegisterUser(string email, string password);
    Task<TokensResponse> AuthorizeUser(string email, string password);
}