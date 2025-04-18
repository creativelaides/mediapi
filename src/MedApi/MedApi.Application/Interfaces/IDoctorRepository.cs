using MedApi.Domain;
using MedApi.Application.Interfaces.Common;

namespace MedApi.Application.Interfaces;

public interface IDoctorRepository : IRepository<Doctor>
{
    Task<IEnumerable<Doctor>> GetBySpecialtyAsync(string specialty);
}