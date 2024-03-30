using AppListaDeCompras.Models;

using FluentValidation;

namespace AppListaDeCompras.Libraries.Validations;

public class AddItemValidator : AbstractValidator<Product>
{
    public AddItemValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("O campo 'nome' é obrigatório")
            .MinimumLength(3).WithMessage("O campo 'nome' deve ter pelo menos 3 caracteres!");

        RuleFor(p => p.Quantity)
            .NotEmpty().WithMessage("O campo 'quantidade' é obrigatório")
            .Must(MoreThenOne).WithMessage("O campo 'quantidade' deve ser maior que 1!");
        
        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("O campo 'preço' é obrigatório!")
            .Must(MoreThenOne).WithMessage("O campo 'preço' deve ser maior que 1!");
    }

    private bool MoreThenOne(decimal quantity)
    {
        return quantity > 0;
    }
}
