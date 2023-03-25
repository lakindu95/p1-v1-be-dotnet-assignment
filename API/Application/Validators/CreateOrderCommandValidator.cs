using System;
using API.Application.Commands;
using FluentValidation;

namespace API.Application.Validators
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required")
								.Length(3).WithMessage("Name should be more than 3 letters");

            RuleFor(c => c.Email).NotEmpty().WithMessage("Email address is required")
								 .EmailAddress().WithMessage("A valid email is required");

			RuleFor(c => c.FlightId).NotEmpty().WithMessage("Flight ID is required");


			RuleFor(c => c.FlightRateId).NotEmpty().WithMessage("Flight Rate ID is required");

			RuleFor(c => c.NoOfSeats).NotEmpty().WithMessage("Number of seats are required")
									 .GreaterThan(0).WithMessage("Number of seats must be 1 or more");
		}
	}
}