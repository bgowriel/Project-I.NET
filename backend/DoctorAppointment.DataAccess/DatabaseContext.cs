using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DoctorAppointment.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DbSet<AdminResponse> Admins => Set<AdminResponse>();

        public DbSet<DoctorResponse> Doctors => Set<DoctorResponse>();

        public DbSet<UserResponse> Users => Set<UserResponse>();

        public DbSet<AppointmentResponse> Appointments => Set<AppointmentResponse>();

        public DbSet<BillResponse> Bills => Set<BillResponse>();

        public DbSet<ServiceProvidedResponse> Services => Set<ServiceProvidedResponse>();

        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=DoctorApp;Trusted_Connection=True;Trust Server Certificate = true")
                .LogTo(Console.Error.WriteLine, LogLevel.Information)//Console.Error.WriteLine  //LogLevel.Error
                .EnableSensitiveDataLogging();//true
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

/*            modelBuilder.Entity<ServiceProvidedResponse>()
                .HasOne(s => s.Doctor)
                .WithMany(d => d.Services)
                .HasForeignKey(s => s.DoctorId);

            modelBuilder.Entity<BillResponse>()
                .HasOne(bill => bill.Payment)
                .WithOne(payment => payment.Bill)
                .HasForeignKey<BillResponse>(bill => bill.PaymentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<PaymentResponse>()
                .HasKey(payment => payment.BillId);

            modelBuilder.Entity<PaymentResponse>()
                .HasOne(payment => payment.Bill)
                .WithOne(bill => bill.Payment)
                .HasForeignKey<PaymentResponse>(payment => payment.BillId);*/
        }

        public void Save()
        {
            SaveChanges();
        }
    }
}
