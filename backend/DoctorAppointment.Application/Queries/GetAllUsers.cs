using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Queries
{
    public class GetAllUsers : IRequest<List<User>>
    {
    }
}
