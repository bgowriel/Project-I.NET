using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Commands
{
    public class DeleteMedicalVisit : IRequest<MedicalVisit>
    {
        public Guid Id { get; set; }
    }
}
