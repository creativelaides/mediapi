using MedApi.Application.Interfaces;
using MedApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace MedApi.Infrastructure.Repositories;

public class PatientRepository : Repository<Patient>, IPatientRepository
{
    public PatientRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Patient?> GetByIdentifierAsync(string identifier)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.Identifier == identifier);
    }
}
