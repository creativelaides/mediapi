using MedApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedApi.Infrastructure.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
       public void Configure(EntityTypeBuilder<Patient> builder)
       {
              builder.ToTable("Patients");

              builder.HasKey(p => p.Id);

              builder.Property(p => p.DateOfBirth)
                    .HasConversion(
                    d => d.ToDateTime(TimeOnly.MinValue),
                    d => DateOnly.FromDateTime(d))
                    .HasColumnType("date");

              builder.Property(p => p.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

              builder.Property(p => p.LastName)
                     .IsRequired()
                     .HasMaxLength(100);

              builder.Property(p => p.Identifier)
                     .IsRequired()
                     .HasMaxLength(50);

              builder.HasMany(p => p.Appointments)
                     .WithOne(a => a.Patient)
                     .HasForeignKey(a => a.PatientId);
       }
}
