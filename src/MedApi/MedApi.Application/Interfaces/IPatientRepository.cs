using MedApi.Domain;
using MedApi.Application.Interfaces.Common;

namespace MedApi.Application.Interfaces;

public interface IPatientRepository : IRepository<Patient>
{
    Task<Patient?> GetByIdentifierAsync(string identifier);
}
