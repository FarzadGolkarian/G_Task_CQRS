using FluentValidation;

namespace G_Task.Application.DTOs.Addresses.Validators
{
    public class CreateAddressValidator : AbstractValidator<CreateAddressDto>
    {
        public CreateAddressValidator()
        {
            Include(new AddressValidator());

            RuleFor(r => r.PersonId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required !")
                .NotNull();
        }
    }
}
