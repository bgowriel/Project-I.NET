using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Queries
{
    public class GetAvailableDateById : IRequest<AvalaibleDate>
    {
        public Guid Id { get; set; }
    }
 
}
