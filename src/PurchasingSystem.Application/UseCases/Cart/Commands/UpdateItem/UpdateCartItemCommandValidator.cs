using FluentValidation;

namespace PurchasingSystem.Application.UseCases.Cart.Commands.UpdateItem
{
    public class UpdateCartItemCommandValidator : AbstractValidator<UpdateCartItemCommand>
    {
        public UpdateCartItemCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId é obrigatório");
            
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId é obrigatório");
            
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantidade deve ser maior que zero");
        }
    }
}
