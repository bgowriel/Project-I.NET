using DoctorAppointment.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DoctorAppointment.DataAccess
{
    public class DatabaseContext : IdentityDbContext<User>
    {
		
		public DbSet<Appointment> Appointments => Set<Appointment>();

        public DbSet<Bill> Bills => Set<Bill>();
        
        public DbSet<MedicalVisit> MedicalVisits => Set<MedicalVisit>();

		public DbSet<Office> Offices => Set<Office>();

		public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=DoctorApp;Trusted_Connection=True;Trust Server Certificate = true")
                .LogTo(Console.Error.WriteLine, LogLevel.Information)//Console.Error.WriteLine  //LogLevel.Error
                .EnableSensitiveDataLogging();//true
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

			//modelBuilder.Entity<Office>()
			//   .HasMany(o => o.Doctors)
			//   .WithOne(d => d.Office)
			//   .HasForeignKey(d => d.OfficeId)
			//   .IsRequired(false)
			//   .OnDelete(DeleteBehavior.SetNull);

			modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(u => u.Appointments)
                .HasForeignKey(a => a.PatientId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<MedicalVisit>()
                .HasOne(mv => mv.Patient)
                .WithMany(p => p.MedicalVisits)
                .HasForeignKey(mv => mv.PatientId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<MedicalVisit>()
                .HasOne(mv => mv.Doctor)
                .WithMany()
                .HasForeignKey(mv => mv.DoctorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Bill>()
                .HasOne(b => b.MedicalVisit)
                .WithOne()
                .HasForeignKey<Bill>(b => b.MedicalVisitId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Patient)
                .WithMany(p => p.Bills)
                .HasForeignKey(b => b.PatientId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Doctor)
                .WithMany()
                .HasForeignKey(b => b.DoctorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
