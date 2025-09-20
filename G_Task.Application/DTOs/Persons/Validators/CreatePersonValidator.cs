using FluentValidation;

namespace G_Task.Application.DTOs.Persons.Validators
{
    public class CreatePersonValidator : AbstractValidator<CreatePersonDto>
    {
        public CreatePersonValidator()
        {
            Include(new PersonValidator());
        }
    }
}
