using Bogus;
using MedApi.Domain;

namespace MedApi.Infrastructure.Seeds;

public static class DoctorSeeder
{
    private static readonly string[] items = ["Medicina General", "Odontología"];

    public static List<Doctor> GenerateDoctors(int count)
    {
        var faker = new Faker<Doctor>()
            .CustomInstantiator(f => new Doctor(
                f.Name.FirstName(),
                f.Name.LastName(),
                f.PickRandom(items),
                f.Phone.PhoneNumber(),
                f.Internet.Email(),
                f.Company.CompanyName()
            ))
            .RuleFor(d => d.Id, _ => Guid.NewGuid());

        return faker.Generate(count);
    }
}