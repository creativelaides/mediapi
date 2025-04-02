using MedApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MedApi.Infrastructure.Seeds;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Doctors.AnyAsync() || await context.Patients.AnyAsync() || await context.Appointments.AnyAsync())
        {
            return; // No ejecuta el seed si ya hay datos
        }

        var doctors = DoctorSeeder.GenerateDoctors(10);
        var patients = PatientSeeder.GeneratePatients(10);
        var appointments = AppointmentSeeder.GenerateAppointments(5, doctors, patients);

        await context.Doctors.AddRangeAsync(doctors);
        await context.Patients.AddRangeAsync(patients);
        await context.Appointments.AddRangeAsync(appointments);

        await context.SaveChangesAsync();
    }
}
