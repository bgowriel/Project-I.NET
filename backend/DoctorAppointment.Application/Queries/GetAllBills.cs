using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Queries
{
    public class GetAllBills : IRequest<List<Bill>>
    {
    }
}
