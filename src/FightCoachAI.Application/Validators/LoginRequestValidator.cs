using FluentValidation;

namespace FightCoachAI.Application.Validators;

public class LoginRequestValidator : AbstractValidator<DTOs.Auth.LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}
