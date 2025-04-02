using MedApi.Domain;

namespace MedApi.Application.Interfaces;

public interface IAppointmentRepository
{
    Task<Appointment?> GetByIdAsync(Guid id);
    Task<IEnumerable<Appointment>> GetAvailableAppointmentsAsync(Guid doctorId);
    Task AddAsync(Appointment appointment);
    Task UpdateAsync(Appointment appointment);
}
