using Microsoft.EntityFrameworkCore;

namespace MedApi.Infrastructure.Seeds;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        try
        {
            // 1. Seed Doctors
            if (!await context.Doctors.AnyAsync())
            {
                var doctors = DoctorSeeder.GenerateDoctors(10);
                await context.Doctors.AddRangeAsync(doctors);
                await context.SaveChangesAsync();
            }

            // 2. Seed Patients
            if (!await context.Patients.AnyAsync())
            {
                var patients = PatientSeeder.GeneratePatients(10);
                await context.Patients.AddRangeAsync(patients);
                await context.SaveChangesAsync();
            }

            // 3. Seed Appointments
            if (!await context.Appointments.AnyAsync())
            {
                var doctors = await context.Doctors.ToListAsync();
                var patients = await context.Patients.ToListAsync();
                var appointments = AppointmentSeeder.GenerateAppointments(5, doctors, patients);
                await context.Appointments.AddRangeAsync(appointments);
                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error en seeding jerárquico", ex);
        }
    }
}
