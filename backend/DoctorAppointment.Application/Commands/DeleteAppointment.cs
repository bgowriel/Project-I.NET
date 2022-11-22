using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Commands
{
    public class DeleteAppointment : IRequest<Appointment>
    {
        public Guid Id { get; set; }
    }
}
