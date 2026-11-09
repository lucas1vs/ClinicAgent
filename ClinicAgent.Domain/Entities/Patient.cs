namespace ClinicAgent.Domain.Entities;

public class Patient
{
    public Guid PatientId { get; private set; }

    // ALTERADO: nome dividido em duas propriedades
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    // ALTERADO: substitui o CPF puro por HMAC
    public string CpfHmac { get; private set; }

    // NOVO: armazena o CPF de forma criptografada e reversível
    public string CpfEncrypted { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public bool IsActive { get; private set; }

    public ICollection<PatientContactAuthorization> ContactAuthorizations { get; private set; }
    = new List<PatientContactAuthorization>();

    public ICollection<Appointment> Appointments { get; private set; }
        = new List<Appointment>();

    // Construtor utilizado na criação de um paciente
    public Patient(
        string firstName,
        string lastName,
        string cpfHmac,
        string cpfEncrypted)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("O primeiro nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("O sobrenome é obrigatório.");

        if (string.IsNullOrWhiteSpace(cpfHmac))
            throw new ArgumentException("O HMAC do CPF é obrigatório.");

        if (string.IsNullOrWhiteSpace(cpfEncrypted))
            throw new ArgumentException("O CPF criptografado é obrigatório.");

        PatientId = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        CpfHmac = cpfHmac;
        CpfEncrypted = cpfEncrypted;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }


}