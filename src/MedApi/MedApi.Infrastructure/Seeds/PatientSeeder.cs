using Bogus;
using MedApi.Domain;

namespace MedApi.Infrastructure.Seeds;

public static class PatientSeeder
{
    public static List<Patient> GeneratePatients(int count)
    {
        var faker = new Faker<Patient>()
            .RuleFor(p => p.Id, _ => Guid.NewGuid())
            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
            .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.Identifier, f => f.Random.Replace("##########"))
            .RuleFor(p => p.Email, (f, p) => f.Internet.Email(p.FirstName, p.LastName))
            .RuleFor(p => p.DateOfBirth, f => f.Date.Past(40, DateTime.UtcNow.AddYears(-18))) // Edad entre 18 y 40
            .RuleFor(p => p.PhoneNumber, f => f.Phone.PhoneNumber());

        return faker.Generate(count);
    }
}
