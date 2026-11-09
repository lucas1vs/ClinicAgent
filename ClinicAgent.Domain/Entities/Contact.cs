namespace ClinicAgent.Domain.Entities;

public class Contact
{
    public Guid ContactId { get; private set; }
    public string PhoneNumber { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsActive { get; private set; }

    public ICollection<PatientContactAuthorization> PatientAuthorizations { get; private set; }
    = new List<PatientContactAuthorization>();

    public ICollection<PatientContactAuthorization> RequestedAuthorizations { get; private set; }
        = new List<PatientContactAuthorization>();

    public ICollection<Appointment> Appointments { get; private set; }
        = new List<Appointment>();

    public Contact(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("O número de telefone é obrigatório.");

        ContactId = Guid.NewGuid();
        PhoneNumber = phoneNumber;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }
}