using DoctorAppointment.Domain.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.DataAccess
{
    public interface IDatabaseContext
    {
        DbSet<AppointmentResponse> Appointments { get; }
        DbSet<DoctorResponse> Doctors { get; }

        void Save();

    }
}