using System.ComponentModel;

namespace FlowersShop.BL.Authorization.Exception;

public enum ResultCode
{
    [Description("User not found.")] UserNotFound = 001,
    [Description("Identity server error.")]
    IdentityServerError = 002,
    [Description("Email or password is incorrect.")]
    EmailOrPasswordIsIncorrect = 003,
    [Description("User already exists.")]
    UserAlreadyExists = 004,
    [Description("User creation failure.")]
    UserCreationFailure = 005,
}