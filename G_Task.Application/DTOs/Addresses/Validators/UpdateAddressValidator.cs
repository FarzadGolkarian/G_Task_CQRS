using FluentValidation;

namespace G_Task.Application.DTOs.Addresses.Validators
{
    public class UpdateAddressValidator : AbstractValidator<UpdateAddressDto>
    {
        public UpdateAddressValidator()
        {
            Include(new AddressValidator());
        }
    }
}
