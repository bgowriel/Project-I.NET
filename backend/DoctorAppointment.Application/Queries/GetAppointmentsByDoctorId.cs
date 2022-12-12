using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Queries
{
    public class GetAppointmentsByDoctorId : IRequest<List<Appointment>>
    {
        public string? DoctorId { get; set; }
    }
}
