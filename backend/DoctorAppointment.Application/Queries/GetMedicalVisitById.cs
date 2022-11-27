using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Queries
{
	public class GetMedicalVisitById : IRequest<MedicalVisit>
    {
        public Guid Id { get; set; }
    }
}
