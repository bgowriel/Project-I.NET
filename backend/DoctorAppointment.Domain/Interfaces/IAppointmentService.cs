using DoctorAppointment.Domain.Models.Request;
using DoctorAppointment.Domain.Models.Response;

namespace DoctorAppointment.Domain.Interfaces
{
    public interface IAppointmentService
    {
        public void AddApointment(AppointmentResponse appointment);
        public List<AppointmentResponse> GetAll();
    }
}
