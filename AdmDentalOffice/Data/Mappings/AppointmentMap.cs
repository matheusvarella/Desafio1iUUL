using AdmDentalOffice.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdmDentalOffice.Data.Mappings
{
    public class AppointmentMap : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointment");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.PatientId)
                .IsRequired()
                .HasColumnName("PatientId")
                .HasColumnType("INT");

            builder.Property(x => x.Cpf)
                .IsRequired()
                .HasColumnName("Cpf")
                .HasColumnType("INT");

            builder.Property(x => x.AppointmentDate)
                .IsRequired()
                .HasColumnName("AppointmentDate")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(10);

            builder.Property(x => x.StartTime)
                .IsRequired()
                .HasColumnName("StartTime")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(4);

            builder.Property(x => x.EndTime)
                .IsRequired()
                .HasColumnName("EndTime")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(4);

            builder.HasOne(x => x.Patient)
                .WithMany(x => x.Appointments)
                .HasConstraintName("FK_Appointment_Patient")
                .HasForeignKey("PatientId")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
