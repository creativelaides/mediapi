using MedApi.Domain.Primitives;

namespace MedApi.Domain;

public class Patient : AggregateRoot
{
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? Identifier { get; private set; }
    public string? Email { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string? PhoneNumber { get; private set; }

    private readonly List<Appointment> _appointments = [];
    public IReadOnlyCollection<Appointment> Appointments => _appointments.AsReadOnly();

    private Patient() { } // Constructor para EF Core

    public Patient(string firstName, string lastName, string identifier, string email, DateTime dateOfBirth, string phoneNumber)
    {
        FirstName = firstName ?? throw new ArgumentException("First Name is required");
        LastName = lastName ?? throw new ArgumentException("Last Name is required");
        Identifier = identifier ?? throw new ArgumentException("Identifier is required");
        Email = email ?? throw new ArgumentException("Email is required");
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber ?? throw new ArgumentException("Phone Number is required");
    }

    public void AddAppointment(Appointment appointment)
    {
        ArgumentNullException.ThrowIfNull(appointment);
        _appointments.Add(appointment);
    }

    // New method for authentication
    public bool Authenticate(string identifier, DateTime dateOfBirth)
    {
        return Identifier == identifier && DateOfBirth.Date == dateOfBirth.Date;
    }
}