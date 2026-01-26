using FluentValidation;

namespace PurchasingSystem.Application.UseCases.Cart.Commands.AddItem
{
    public class AddItemToCartCommandValidator : AbstractValidator<AddItemToCartCommand>
    {
        public AddItemToCartCommandValidator()
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
