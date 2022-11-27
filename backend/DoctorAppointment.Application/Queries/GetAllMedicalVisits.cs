using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Queries
{
    public class GetAllMedicalVisits : IRequest<List<MedicalVisit>>
    {
    }
}
