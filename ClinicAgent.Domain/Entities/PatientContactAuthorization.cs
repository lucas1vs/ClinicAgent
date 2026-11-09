using ClinicAgent.Domain.Enums;
namespace ClinicAgent.Domain.Entities;

public class PatientContactAuthorization
{
    public Guid PatientContactAuthorizationId { get; private set; }
    public Guid PatientId { get; private set; }
    public Guid ContactId { get; private set; }
    public Guid? RequestedByContactId { get; private set; }

    public AuthorizationStatus Status { get; private set; }

    public DateTime RequestedAt { get; private set; }
    public DateTime? RespondedAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }

    public Patient Patient { get; private set; } = null!;

    public Contact Contact { get; private set; } = null!;

    public Contact? RequestedByContact { get; private set; }

    public PatientContactAuthorization(
        Guid patientId,
        Guid contactId,
        Guid? requestedByContactId)
    {
        PatientContactAuthorizationId = Guid.NewGuid();
        PatientId = patientId;
        ContactId = contactId;
        RequestedByContactId = requestedByContactId;

        Status = AuthorizationStatus.Pending;
        RequestedAt = DateTime.UtcNow;
    }

    public void Accept()
    {
        if (Status != AuthorizationStatus.Pending)
        {
            throw new InvalidOperationException("Não é possível aceitar uma autorização que não esteja pendente..");
           
        }

        Status = AuthorizationStatus.Authorized;
        RespondedAt = DateTime.UtcNow;
    }

    public void Rejected()
    {
        if (Status != AuthorizationStatus.Pending)
        {
            throw new InvalidOperationException("Não é possível aceitar uma autorização que não esteja pendente..");

        }

        Status = AuthorizationStatus.Rejected;
        RespondedAt = DateTime.UtcNow;
    }

    public void Revoke()
    {
        if (Status != AuthorizationStatus.Authorized)
        {
            throw new InvalidOperationException("Não é possível revogar uma autorização que não esteja autorizada.");
        }
        Status = AuthorizationStatus.Revoked;
        RevokedAt = DateTime.UtcNow;
    }   
}
