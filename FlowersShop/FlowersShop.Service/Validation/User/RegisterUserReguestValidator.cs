using FlowersShop.Service.Controllers.Entities.UserEntities;
using FluentValidation;

namespace FlowersShop.Service.Validation.User;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.PasswordHash)
            .NotEmpty()
            .WithMessage("Password is required");
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email is required");
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(200)
            .WithMessage("Name is required");
        RuleFor(x => x.Surname)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(200)
            .WithMessage("Surname is required");
        RuleFor(x => x.Patronymic)
            .MinimumLength(0)
            .MaximumLength(200)
            .WithMessage("Patronymic is required");
        RuleFor(x => x.RoleId)
            .NotEmpty()
            .WithMessage("Permission is required");
    }
}