using DoctorAppointment.Domain.Models.Response;

namespace DoctorAppointment.Domain.Interfaces
{
    public interface IAppointmentRepository
    {
        public void AddApointment(AppointmentResponse appointment);

        public List<AppointmentResponse> GetAll();

        public void Save();
    }
}
