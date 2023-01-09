using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Queries
{
    public class GetBillsByDoctorId : IRequest<List<Bill>>
    {
        public string? DoctorId  { get; set; }
    }
}
