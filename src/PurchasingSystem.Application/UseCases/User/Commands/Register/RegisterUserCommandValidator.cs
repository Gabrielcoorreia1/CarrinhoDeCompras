using FluentValidation;
using PurchasingSystem.Domain.User.Errors;

namespace PurchasingSystem.Application.UseCases.User.Commands.Register
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MinimumLength(2).WithMessage("O nome deve ter no mínimo 2 caracteres.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Sobrenome é obrigatório")
                .MinimumLength(2).WithMessage("O sobrenome deve ter no mínimo 2 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithErrorCode(DomainErrors.Email.Empty.Code).WithMessage(DomainErrors.Email.Empty.Message)
                .EmailAddress().WithErrorCode(DomainErrors.Email.InvalidFormat.Code).WithMessage(DomainErrors.Email.InvalidFormat.Message);

            RuleFor(x => x.Password)
                .NotEmpty().WithErrorCode(DomainErrors.Password.Empty.Code).WithMessage(DomainErrors.Password.Empty.Message)
                .MinimumLength(8).WithErrorCode(DomainErrors.Password.InvalidFormat.Code).WithMessage(DomainErrors.Password.InvalidFormat.Message);

            RuleFor(x => x.Cpf)
                .NotEmpty().WithErrorCode(DomainErrors.Cpf.Empty.Code).WithMessage(DomainErrors.Cpf.Empty.Message)
                .Length(11).WithMessage("O CPF deve conter 11 dígitos.")
                .Matches("^[0-9]*$").WithMessage("O CPF deve conter apenas números.");
        }
    }
}
