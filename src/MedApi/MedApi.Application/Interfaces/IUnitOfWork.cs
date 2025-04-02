namespace MedApi.Application.Interfaces;

public interface IUnitOfWork
{
    IAppointmentRepository Appointments { get; }
    IDoctorRepository Doctors { get; }
    IPatientRepository Patients { get; }
    Task<int> SaveChangesAsync();
}
