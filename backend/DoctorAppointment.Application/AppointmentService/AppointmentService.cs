using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.Application.AppointmentService
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
        }
        public Guid AddApointment(AppointmentRequest appointment)
        {
            throw new NotImplementedException();
        }
    }
}
