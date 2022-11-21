using DoctorAppointment.Domain.Helpers;
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

        public Result AddApointment(AppointmentResponse appointment)
        {
            if (appointment == null)
                return Result.Failure("Appointment can't be NULL");

            appointmentRepository.AddApointment(appointment);
            appointmentRepository.Save();

            return Result.Success();
            
        }

        public List<AppointmentResponse> GetAll()
        {
            return appointmentRepository.GetAll();
        }
    }
}
