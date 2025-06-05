using FluentValidation;

namespace Application.IdentityProvider.WebApi.Features.Register;

internal class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email address");
    }
}