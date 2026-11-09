
using ClinicAgent.Domain.Entities;
using FluentValidation;

namespace ClinicAgent.Application.Validators;

public class PatientContactAuthorizationValidator
    : AbstractValidator<PatientContactAuthorization>
{
    public PatientContactAuthorizationValidator()
    {
        RuleFor(a => a.PatientId)
            .NotEmpty()
            .WithMessage("O paciente é obrigatório.");

        RuleFor(a => a.ContactId)
            .NotEmpty()
            .WithMessage("O contato é obrigatório.");

        RuleFor(a => a.RequestedByContactId)
            .Must(id => id == null || id != Guid.Empty)
            .WithMessage("O contato solicitante informado é inválido.");
    }
}
