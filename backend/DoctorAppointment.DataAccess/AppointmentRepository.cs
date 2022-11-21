using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.DataAccess
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public Guid AddApointment(AppointmentRequest appointment)
        {
            return new Guid();
        }
    }
}
