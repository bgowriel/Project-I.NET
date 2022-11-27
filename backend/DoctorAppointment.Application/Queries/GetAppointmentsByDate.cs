using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Queries
{
    public class GetAppointmentsByDate : IRequest<List<Appointment>>
    {
        public DateTime Date { get; set; }
    }
}
