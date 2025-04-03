using Bogus;
using MedApi.Domain;
using MedApi.Domain.ObjectsValues;

namespace MedApi.Infrastructure.Seeds;

public static class AppointmentSeeder
{
    public static List<Appointment> GenerateAppointments(
        int count,
        List<Doctor> doctors,
        List<Patient> patients)
    {
        var specialties = new[] { "Medicina General", "Odontología" };

        var faker = new Faker<Appointment>()
            .CustomInstantiator(f => new Appointment(
                f.Date.Soon(30),
                f.PickRandom(specialties),
                f.PickRandom(doctors.Where(d =>
                    d.Specialization == "Medicina General" ||
                    d.Specialization == "Odontología")),
                f.PickRandom(patients)
            ))
            .RuleFor(a => a.Id, _ => Guid.NewGuid())
            .RuleFor(a => a.Status, _ => Status.Available);

        return faker.Generate(count);
    }
}