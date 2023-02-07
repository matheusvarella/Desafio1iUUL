using AdmDentalOffice.Data.Mappings;
using AdmDentalOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace AdmDentalOffice.Data
{
    public class AdmDentalOfficeDataContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=AdmDentalOffice;User ID=sa;password=1q2w3e4r@#$");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppointmentMap());
            modelBuilder.ApplyConfiguration(new PatientMap());
        }
    }
}
