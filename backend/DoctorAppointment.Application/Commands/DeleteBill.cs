using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Commands
{
    public class DeleteBill : IRequest<Bill>
    {
        public Guid Id { get; set; }
    }
}
