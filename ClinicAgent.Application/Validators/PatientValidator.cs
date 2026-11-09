
using ClinicAgent.Domain.Entities;
using FluentValidation;

namespace ClinicAgent.Application.Validators;

public class PatientValidator : AbstractValidator<Patient>
{
    public PatientValidator()
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

        RuleFor(p => p.CpfHmac)
            .NotEmpty()
            .WithMessage("O HMAC do CPF é obrigatório.")
            .MaximumLength(64)
            .WithMessage("O HMAC do CPF deve ter no máximo 64 caracteres.");

        RuleFor(p => p.CpfEncrypted)
            .NotEmpty()
            .WithMessage("O CPF criptografado é obrigatório.")
            .MaximumLength(512)
            .WithMessage("O CPF criptografado deve ter no máximo 512 caracteres.");
    }
}
