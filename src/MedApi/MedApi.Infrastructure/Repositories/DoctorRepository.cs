using MedApi.Domain;
using MedApi.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedApi.Infrastructure.Repositories;

public class DoctorRepository : Repository<Doctor>, IDoctorRepository
{
    public DoctorRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Doctor>> GetBySpecialtyAsync(string specialty)
    {
        return await _dbSet
            .Where(d => d.Specialization == specialty)
            .ToListAsync();
    }
}