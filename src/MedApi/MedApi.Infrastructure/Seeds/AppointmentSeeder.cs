using Bogus;
using MedApi.Domain;
using MedApi.Domain.ObjectsValues;

namespace MedApi.Infrastructure.Seeds;

public static class AppointmentSeeder
{
    public static List<Appointment> GenerateAppointments(int count, List<Doctor> doctors, List<Patient> patients)
    {
        var faker = new Faker<Appointment>()
            .RuleFor(a => a.Id, _ => Guid.NewGuid())
            .RuleFor(a => a.AppointmentDate, f => f.Date.Soon(30))
            .RuleFor(a => a.Status, _ => Status.Avalible)
            .RuleFor(a => a.DoctorId, f => f.PickRandom(doctors).Id)
            .RuleFor(a => a.PatientId, f => f.PickRandom(patients).Id);

        return faker.Generate(count);
    }
}
