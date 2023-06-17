using FluentValidation;

namespace ProductApp.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.ManufactureEmail)
          .MaximumLength(200)
          .NotEmpty();

        RuleFor(v => v.ManufacturePhone)
          .MaximumLength(50)
          .NotEmpty();

        RuleFor(v => v.ProduceDate)
          .Must(x=>x!=DateTime.MinValue && x != DateTime.MaxValue)
          .NotEmpty();
    }
}
