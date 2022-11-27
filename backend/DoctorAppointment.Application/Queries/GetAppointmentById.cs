using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Queries
{
    public class GetAppointmentById : IRequest<Appointment>
    {
        public Guid Id { get; set; }
    }
}
