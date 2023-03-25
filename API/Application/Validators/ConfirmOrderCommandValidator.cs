using API.Application.Commands;
using FluentValidation;

namespace API.Application.Validators
{
    public class ConfirmOrderCommandValidator : AbstractValidator<ConfirmOrderCommand>
    {
        public ConfirmOrderCommandValidator()
        {
            RuleFor(o => o.Id).NotEmpty().WithMessage("Order ID is required");
        }
    }
}