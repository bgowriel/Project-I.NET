using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Queries
{
    public class GetBillById : IRequest<Bill>
    {
        public Guid Id { get; set; }
    }
}
