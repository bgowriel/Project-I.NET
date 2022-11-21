using DoctorAppointment.DataAccess;
using DoctorAppointment.Domain.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Infrastructure
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<AppointmentResponse> Appointments => Set<AppointmentResponse>();
        public DbSet<DoctorResponse> Doctors => Set<DoctorResponse>();
        
        public void Save()
        {
            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = DoctorAppointment.db");
        }
    }
}