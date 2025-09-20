using FluentValidation;
using G_Task.Application.DTOs.Persons.Interfaces;

namespace G_Task.Application.DTOs.Persons.Validators
{
    public class PersonValidator : AbstractValidator<IPerson>
    {
        public PersonValidator()
        {

            RuleFor(r => r.FirstName)
                .NotEmpty()
                .WithMessage("{PropertyName} is required !")
                .MaximumLength(50)
                .NotNull();


            RuleFor(r => r.LastName)
                .NotEmpty()
                .WithMessage("{PropertyName} is required !")
                .MaximumLength(100)
                .NotNull();


            RuleFor(r => r.NationalCode)
                .NotEmpty()
                .WithMessage("{PropertyName} is required !")
                .MinimumLength(10)
                .MaximumLength(10)
                .NotNull();
        }

    }
}
