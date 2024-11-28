using FluentValidation;

namespace KeepMoney.Application.Tokens.Commands.RegisterUserCommand;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.FirstName).MinimumLength(2).MaximumLength(32);
        RuleFor(x => x.LastName).MinimumLength(2).MaximumLength(32);
        RuleFor(x => x.Email).EmailAddress();
    }
}