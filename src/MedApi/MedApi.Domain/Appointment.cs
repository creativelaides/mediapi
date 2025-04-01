using MedApi.Domain.ObjectsValues;
using MedApi.Domain.Primitives;

namespace MedApi.Domain;

public class Appointment : AggregateRoot
{
    public DateTime AppointmentDate { get; private set; }
    public Status Status { get; private set; }

    public Guid DoctorId { get; private set; }
    public Doctor? Doctor { get; private set; }

    public Guid PatientId { get; private set; }
    public Patient? Patient { get; private set; }

    private Appointment() { } // Constructor para EF Core

    public Appointment(DateTime appointmentDate, Doctor doctor, Patient patient)
    {
        if (doctor is null || patient is null)
            throw new ArgumentException("Doctor and Patient are required");

        AppointmentDate = appointmentDate;
        Status = Status.Available;
        Doctor = doctor;
        DoctorId = doctor.Id;
        Patient = patient;
        PatientId = patient.Id;
    }

    public void Reserve() => Status = Status.Reserved;
    public void Complete() => Status = Status.Finished;
    public void Cancel() => Status = Status.Cancelled;
}
