using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Queries
{
    public class GetAllAppointments : IRequest<List<Appointment>>
    {
    }
}
