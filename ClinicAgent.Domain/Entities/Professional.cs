
namespace ClinicAgent.Domain.Entities;

public class Professional
{
    public Guid ProfessionalId { get; private set; }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Specialty { get; private set; }
    public string Email { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public bool IsActive { get; private set; }

    public ICollection<Appointment> Appointments { get; private set; }
        = new List<Appointment>();

    public Professional(
        string firstName,
        string lastName,
        string specialty,
        string email)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("O primeiro nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("O sobrenome é obrigatório.");

        if (string.IsNullOrWhiteSpace(specialty))
            throw new ArgumentException("A especialidade é obrigatória.");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("O e-mail é obrigatório.");

        ProfessionalId = Guid.NewGuid();

        FirstName = firstName;
        LastName = lastName;
        Specialty = specialty;
        Email = email.Trim();

        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }
}
