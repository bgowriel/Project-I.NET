using DoctorAppointment.Domain.Helpers;
using DoctorAppointment.Domain.Interfaces;
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

        public Result<AppointmentResponse> AddApointment(AppointmentResponse appointment)
        {
            if (appointment == null)
                return Result<AppointmentResponse>.Failure("Appointment can't be NULL");

            if(appointment.Name.Length < 4)
                return Result<AppointmentResponse>.Failure("Appointment name length should be greater than 3");

            appointmentRepository.AddApointment(appointment);
            appointmentRepository.Save();

            return Result<AppointmentResponse>.Success(appointment);
            
        }

        public List<AppointmentResponse> GetAll()
        {
            return appointmentRepository.GetAll();
        }
    }
}
