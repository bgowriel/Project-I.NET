using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.Domain.Interfaces
{
    public interface IDoctorRepository
    {
        public Guid AddDoctor(DoctorRequest doctor);
    }
}
