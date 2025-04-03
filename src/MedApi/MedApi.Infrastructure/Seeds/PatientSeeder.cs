using Bogus;
using MedApi.Domain;

namespace MedApi.Infrastructure.Seeds;

public static class PatientSeeder
{
    public static List<Patient> GeneratePatients(int count)
    {
        var faker = new Faker<Patient>()
            .CustomInstantiator(f => new Patient(
                f.Name.FirstName(),
                f.Name.LastName(),
                f.Random.Replace("##########"),
                f.Internet.Email(),
                f.Date.Past(40, DateTime.UtcNow.AddYears(-18)),
                f.Phone.PhoneNumber()
            ))
            .RuleFor(p => p.Id, _ => Guid.NewGuid());

        return faker.Generate(count);
    }
}
