using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;
using DoctorAppointment.Domain.Models.Response;

namespace DoctorAppointment.Application.AppointmentService
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
        }

        public void AddApointment(AppointmentResponse appointment)
        {
            appointmentRepository.AddApointment(appointment);
            appointmentRepository.Save();
            
        }

        public List<AppointmentResponse> GetAll()
        {
            return appointmentRepository.GetAll();
        }
    }
}
