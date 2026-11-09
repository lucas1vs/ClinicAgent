
using ClinicAgent.Domain.Enums;

namespace ClinicAgent.Domain.Entities;

public class Appointment
{
    public Guid AppointmentId { get; private set; }

    public Guid PatientId { get; private set; }
    public Guid ContactId { get; private set; }
    public Guid ProfessionalId { get; private set; }

    public DateTime StartAt { get; private set; }
    public DateTime EndAt { get; private set; }

    public AppointmentStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public string? GoogleCalendarId { get; private set; }
    public string? GoogleEventId { get; private set; }

    public Patient Patient { get; private set; } = null!;
    public Contact Contact { get; private set; } = null!;
    public Professional Professional { get; private set; } = null!;

    public Appointment? RescheduledFromAppointment { get; private set; }
    public Appointment? RescheduledToAppointment { get; private set; }

    // A nova consulta referencia a consulta anterior.
    public Guid? RescheduledFromAppointmentId { get; private set; }

    public Appointment(
        Guid patientId,
        Guid contactId,
        Guid professionalId,
        DateTime startAt,
        DateTime endAt)
    {
        if (patientId == Guid.Empty)
            throw new ArgumentException("O paciente é obrigatório.");

        if (contactId == Guid.Empty)
            throw new ArgumentException("O contato é obrigatório.");

        if (professionalId == Guid.Empty)
            throw new ArgumentException("O profissional é obrigatório.");

        if (startAt.Kind != DateTimeKind.Utc || endAt.Kind != DateTimeKind.Utc)
            throw new ArgumentException("As datas da consulta devem estar em UTC.");

        if (endAt <= startAt)
            throw new ArgumentException("O horário final deve ser posterior ao inicial.");

        AppointmentId = Guid.NewGuid();

        PatientId = patientId;
        ContactId = contactId;
        ProfessionalId = professionalId;

        StartAt = startAt;
        EndAt = endAt;

        Status = AppointmentStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public void Confirm()
    {
        if (Status != AppointmentStatus.Pending)
            throw new InvalidOperationException(
                "Não é possível confirmar uma consulta que não esteja pendente.");

        Status = AppointmentStatus.Confirmed;
    }

    public void Cancel()
    {
        if (Status != AppointmentStatus.Pending &&
            Status != AppointmentStatus.Confirmed)
            throw new InvalidOperationException(
                "Não é possível cancelar uma consulta neste estado.");

        Status = AppointmentStatus.Cancelled;
    }

    public void MarkAsRescheduled()
    {
        if (Status != AppointmentStatus.Pending &&
            Status != AppointmentStatus.Confirmed)
            throw new InvalidOperationException(
                "Não é possível remarcar uma consulta neste estado.");

        Status = AppointmentStatus.Rescheduled;
    }

    public void MarkAsExpired()
    {
        if (Status != AppointmentStatus.Pending)
            throw new InvalidOperationException(
                "Não é possível expirar uma consulta que não esteja pendente.");

        Status = AppointmentStatus.Expired;
    }

    public void MarkAsFailed()
    {
        if (Status != AppointmentStatus.Pending)
            throw new InvalidOperationException(
                "Não é possível marcar como falha uma consulta que não esteja pendente.");

        Status = AppointmentStatus.Failed;
    }
}
