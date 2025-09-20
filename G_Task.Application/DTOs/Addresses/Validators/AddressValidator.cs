using FluentValidation;
using G_Task.Application.DTOs.Addresses.Interfaces;

namespace G_Task.Application.DTOs.Addresses.Validators
{
    public class AddressValidator : AbstractValidator<IAddress>
    {
        public AddressValidator()
        {

            RuleFor(r => r.PersonAddress)
                    .NotEmpty()
                    .WithMessage("{PropertyName} is required !")
                    .MaximumLength(250)
                    .NotNull();


            RuleFor(r => r.AddressType)
                    .NotEmpty()
                    .WithMessage("{PropertyName} is required !")
                    .NotNull();

        }
    }
}
