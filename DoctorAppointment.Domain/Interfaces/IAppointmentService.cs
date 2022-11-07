using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.Domain.Interfaces
{
    public interface IAppointmentService
    {
        public Guid AddApointment(Appointment appointment);
    }
}
