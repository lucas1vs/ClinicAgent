
using ClinicAgent.Domain.Entities;
using FluentValidation;

namespace ClinicAgent.Application.Validators;

public class AppointmentValidator : AbstractValidator<Appointment>
{
    public AppointmentValidator()
    {
        RuleFor(a => a.PatientId)
            .NotEmpty()
            .WithMessage("O paciente é obrigatório.");

        RuleFor(a => a.ContactId)
            .NotEmpty()
            .WithMessage("O contato é obrigatório.");

        RuleFor(a => a.ProfessionalId)
            .NotEmpty()
            .WithMessage("O profissional é obrigatório.");

        RuleFor(a => a.StartAt)
            .Must(date => date.Kind == DateTimeKind.Utc)
            .WithMessage("A data inicial deve estar em UTC.");

        RuleFor(a => a.EndAt)
            .Must(date => date.Kind == DateTimeKind.Utc)
            .WithMessage("A data final deve estar em UTC.");

        RuleFor(a => a.EndAt)
            .GreaterThan(a => a.StartAt)
            .WithMessage("O horário final deve ser posterior ao inicial.");
    }
}
