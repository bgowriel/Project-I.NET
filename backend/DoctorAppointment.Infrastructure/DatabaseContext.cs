using DoctorAppointment.DataAccess;
using DoctorAppointment.Domain.Models.Response;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DoctorAppointment.Infrastructure
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<AppointmentResponse> Appointments => Set<AppointmentResponse>();
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