using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Queries
{
    public class GetBillsByPatientId: IRequest<List<Bill>>
    {
        public string? PatientId { get; set; }
    }
}
