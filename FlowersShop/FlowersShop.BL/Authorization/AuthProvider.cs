using AutoMapper;
using Duende.IdentityServer.Models;
using FlowersShop.BL.Authorization.Entities;
using FlowersShop.BL.Authorization.Exception;
using FlowersShop.BL.Users.Entity;
using FlowersShop.DataAccess.Entities;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;

namespace FlowersShop.BL.Authorization;

public class AuthProvider(
    SignInManager<UserEntity> signInManager,
    UserManager<UserEntity> userManager,
    IHttpClientFactory httpClientFactory,
    string identityServerUri,
    string clientId,
    string clientSecret,
    IMapper mapper) : IAuthProvider
{
    public async Task<TokensResponse> AuthorizeUser(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            throw new AuthException(ResultCode.UserNotFound);
        }
        var verificationPasswordResult = await signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!verificationPasswordResult.Succeeded)
        {
            throw new AuthException(ResultCode.EmailOrPasswordIsIncorrect);
        }
        var client = httpClientFactory.CreateClient();
        var discoveryDoc = await client.GetDiscoveryDocumentAsync(identityServerUri);
        if (discoveryDoc.IsError)
        {
            throw new AuthException(ResultCode.IdentityServerError);
        }
        var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
        {
            Address = discoveryDoc.TokenEndpoint,
            GrantType = GrantType.ResourceOwnerPassword,
            ClientId = clientId,
            ClientSecret = clientSecret,
            UserName = user.UserName!,
            Password = password,
            Scope = "api offline_access"
        });
        if (tokenResponse.IsError)
        {
            throw new System.Exception();
        }
        return new TokensResponse
        {
            AccessToken = tokenResponse.AccessToken,
            RefreshToken = tokenResponse.RefreshToken,
        };
    }
    public async Task<UserModel> RegisterUser(string email, string password)
    {
        var existingUser = await userManager.FindByEmailAsync(email);
        if (existingUser != null)
        {
            throw new AuthException(ResultCode.UserAlreadyExists);
        }
        var user = new UserEntity
        {
            Email = email
        };
        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            throw new AuthException(ResultCode.UserCreationFailure);
        }
        var createdUser = await userManager.FindByEmailAsync(email);
        return mapper.Map<UserModel>(createdUser);
    }
}