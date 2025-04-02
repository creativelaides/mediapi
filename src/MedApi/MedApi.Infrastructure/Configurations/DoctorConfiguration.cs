using MedApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedApi.Infrastructure.Configurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctor");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.FirstName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(d => d.LastName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(d => d.Specialization)
               .IsRequired()
               .HasMaxLength(150);

        builder.HasMany(d => d.Appointments)
               .WithOne(a => a.Doctor)
               .HasForeignKey(a => a.DoctorId);
    }
}
