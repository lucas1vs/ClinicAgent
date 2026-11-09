
using ClinicAgent.Domain.Entities;
using FluentValidation;

namespace ClinicAgent.Application.Validators;

public class ContactValidator : AbstractValidator<Contact>
{
    public ContactValidator()
    {
        RuleFor(c => c.PhoneNumber)
            .NotEmpty()
            .WithMessage("O número de telefone é obrigatório.")
            .MaximumLength(20)
            .WithMessage("O número de telefone deve ter no máximo 20 caracteres.");
    }
}
