using MedApi.Domain;

namespace MedApi.Application.Interfaces;

public interface IPatientRepository
{
    Task<Patient?> GetByIdentifierAsync(string identifier);
}
