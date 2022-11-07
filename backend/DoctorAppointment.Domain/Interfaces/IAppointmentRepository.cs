using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.Domain.Interfaces
{
    public interface IAppointmentRepository
    {
        public Guid AddApointment(AppointmentRequest appointment);
    }
}
