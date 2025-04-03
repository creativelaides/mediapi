using MedApi.Domain.ObjectsValues;
using MedApi.Domain.Primitives;

namespace MedApi.Domain;

public class Appointment : AggregateRoot
{
    public DateTime AppointmentDate { get; private set; }
    public Status Status { get; private set; }
    public string? Specialty { get; private set; }

    public Guid DoctorId { get; private set; }
    public Doctor? Doctor { get; private set; }

    public Guid PatientId { get; private set; }
    public Patient? Patient { get; private set; }

    protected Appointment() { }

    public Appointment(
        DateTime appointmentDate,
        string specialty,
        Doctor doctor,
        Patient patient)
    {
        if (doctor == null || patient == null)
            throw new ArgumentException("Doctor and Patient are required");

        AppointmentDate = appointmentDate;
        Specialty = specialty ?? throw new ArgumentException("Specialty is required");
        Status = Status.Available;
        Doctor = doctor;
        DoctorId = doctor.Id;
        Patient = patient;
        PatientId = patient.Id;
    }
    public void AssignPatient(Patient patient)
    {
        if (patient is null)
            throw new ArgumentException("Patient is required");

        if (Status != Status.Available)
            throw new InvalidOperationException("Only available appointments can be assigned");

        Patient = patient;
        PatientId = patient.Id;
    }
    public void Reserve()
    {
        if (Status != Status.Available || Patient is null)
            throw new InvalidOperationException("Appointment must be available and have a patient assigned");

        Status = Status.Reserved;
        SetUpdated(); // Llama al método protegido
    }

    public void Cancel()
    {
        Status = Status.Cancelled;
        SetUpdated();
    }

    public void Complete()
    {
        Status = Status.Finished;
        SetUpdated();
    }
}
