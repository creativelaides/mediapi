using MedApi.Domain;
using MedApi.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedApi.Infrastructure.Repositories;

public class PatientRepository : Repository<Patient>, IPatientRepository
{
    public PatientRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Patient?> GetByIdentifierAsync(string identifier)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.Identifier == identifier);
    }

    public async Task<Patient?> AuthenticateAsync(string identifier, DateOnly dateOfBirth)
    {
        return await _dbSet.FirstOrDefaultAsync(p => 
            p.Identifier == identifier && 
            p.DateOfBirth == dateOfBirth);
    }
}