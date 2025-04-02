using Bogus;
using MedApi.Domain;

namespace MedApi.Infrastructure.Seeds;

public static class DoctorSeeder
{
    public static List<Doctor> GenerateDoctors(int count)
    {
        var faker = new Faker<Doctor>()
            .RuleFor(d => d.Id, _ => Guid.NewGuid())
            .RuleFor(d => d.FirstName, f => f.Name.FirstName())
            .RuleFor(d => d.LastName, f => f.Name.LastName())
            .RuleFor(d => d.Specialization, f => f.PickRandom(new[] { "Medicina General", "Odontología" }))
            .RuleFor(d => d.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(d => d.Email, (f, d) => f.Internet.Email(d.FirstName, d.LastName))
            .RuleFor(d => d.MedicalCenter, f => f.Company.CompanyName());

        return faker.Generate(count);
    }
}
