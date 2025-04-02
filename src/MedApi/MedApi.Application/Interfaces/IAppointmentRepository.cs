using MedApi.Domain;
using MedApi.Application.Interfaces.Common;

namespace MedApi.Application.Interfaces;

public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<IEnumerable<Appointment>> GetAvailableAppointmentsAsync(Guid doctorId);
}
