using MedApi.Application.Interfaces;
using MedApi.Domain.Exceptions;
using MedApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MedApi.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IAppointmentRepository Appointments { get; }
    public IDoctorRepository Doctors { get; }
    public IPatientRepository Patients { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Appointments = new AppointmentRepository(_context);
        Doctors = new DoctorRepository(_context);
        Patients = new PatientRepository(_context);
    }

    public async Task<int> SaveChangesAsync()
    {
        try
        {
            return await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency conflict occurred", ex);
        }
    }


}
