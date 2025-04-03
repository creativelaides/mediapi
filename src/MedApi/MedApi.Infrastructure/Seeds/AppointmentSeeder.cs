using Bogus;
using MedApi.Domain;
using MedApi.Domain.ObjectsValues;

namespace MedApi.Infrastructure.Seeds;

public static class AppointmentSeeder
{
    public static List<Appointment> GenerateAppointments(int count, List<Doctor> doctors, List<Patient> patients)
    {
        var specialties = new[] { "Medicina General", "Odontología" };
        
        var faker = new Faker<Appointment>()
            .RuleFor(a => a.Id, _ => Guid.NewGuid())
            .RuleFor(a => a.AppointmentDate, f => f.Date.Soon(30))
            .RuleFor(a => a.Status, _ => Status.Available)
            .RuleFor(a => a.Specialty, f => f.PickRandom(specialties))
            .RuleFor(a => a.DoctorId, f => f.PickRandom(doctors.Where(d => d.Specialization == specialties[0] || d.Specialization == specialties[1]).ToList()).Id)
            .RuleFor(a => a.PatientId, f => f.PickRandom(patients).Id);

        return faker.Generate(count);
    }
}