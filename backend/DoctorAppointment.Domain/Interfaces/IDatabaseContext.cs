using DoctorAppointment.Domain.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Domain.Interfaces
{
    public interface IDatabaseContext
    {
        DbSet<AdminResponse> Admins { get; }

        DbSet<DoctorResponse> Doctors { get; }
        
        DbSet<UserResponse> Users { get; }

        //DbSet<AppointmentResponse> Appointments { get; }

        //DbSet<BillResponse> Bills { get; }

        //DbSet<PaymentResponse> Payments { get; }

        //DbSet<ServiceProvidedResponse> Services { get; }

        //DbSet<MedicalVisitResponse> MedicalVisits { get; }

        void Save();
    }
}
