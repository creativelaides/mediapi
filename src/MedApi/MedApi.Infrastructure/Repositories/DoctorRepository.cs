using MedApi.Application.Interfaces;
using MedApi.Domain;

namespace MedApi.Infrastructure.Repositories;

public class DoctorRepository : Repository<Doctor>, IDoctorRepository
{
    public DoctorRepository(ApplicationDbContext context) : base(context) { }
}
