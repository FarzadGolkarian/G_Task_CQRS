using FluentValidation;

namespace G_Task.Application.DTOs.Persons.Validators
{
    public class UpdatePersonValidator : AbstractValidator<UpdatePersonDto>
    {
        public UpdatePersonValidator()
        {
            Include(new PersonValidator());
        }

    }
}
