using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.Domain.Interfaces
{
    public interface IDoctorService
    {
        public Guid AddDoctor(DoctorRequest doctor);
    }
}
