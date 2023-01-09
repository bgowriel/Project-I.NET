using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Queries
{
    public class GetUserById : IRequest<User>
    {
        public string? Id { get; set; }
    }
}
