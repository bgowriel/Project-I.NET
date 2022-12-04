using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Queries
{
    public class GetAppointmentsByPatientId : IRequest<List<Appointment>>
    {
        public string PatientId { get; set; }
    }
}
