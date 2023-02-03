using AdmDentalOffice.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdmDentalOffice.Data.Mappings
{
    public class PatientMap : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patient");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Cpf)
                .IsRequired()
                .HasColumnName("Cpf")
                .HasColumnType("INT");

            builder.Property(x => x.BirthDate)
                .IsRequired()
                .HasColumnName("BirthDate")
                .HasColumnType("SMALLDATETIME")
                .HasMaxLength(60);

            builder.HasMany(x => x.Appointments)
                .WithOne(x => x.Patient)
                .HasConstraintName("FK_Patient_Appointment")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
