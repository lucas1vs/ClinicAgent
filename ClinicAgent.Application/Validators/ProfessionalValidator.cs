
using ClinicAgent.Domain.Entities;
using FluentValidation;

namespace ClinicAgent.Application.Validators;

public class ProfessionalValidator : AbstractValidator<Professional>
{
    public ProfessionalValidator()
    {
        RuleFor(p => p.FirstName)
            .NotEmpty()
            .WithMessage("O primeiro nome é obrigatório.")
            .MaximumLength(100)
            .WithMessage("O primeiro nome deve ter no máximo 100 caracteres.");

        RuleFor(p => p.LastName)
            .NotEmpty()
            .WithMessage("O sobrenome é obrigatório.")
            .MaximumLength(100)
            .WithMessage("O sobrenome deve ter no máximo 100 caracteres.");

        RuleFor(p => p.Specialty)
            .NotEmpty()
            .WithMessage("A especialidade é obrigatória.")
            .MaximumLength(100)
            .WithMessage("A especialidade deve ter no máximo 100 caracteres.");

        RuleFor(p => p.Email)
            .NotEmpty()
            .WithMessage("O e-mail é obrigatório.")
            .EmailAddress()
            .WithMessage("Informe um endereço de e-mail válido.")
            .MaximumLength(256)
            .WithMessage("O e-mail deve ter no máximo 256 caracteres.");
    }
}
