using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Queries
{
    public class GetAllAvailableDates : IRequest<List<AvalaibleDate>>
    {
    }
}
