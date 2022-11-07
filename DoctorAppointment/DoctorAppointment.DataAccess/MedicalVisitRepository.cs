using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.DataAccess
{
    public class MedicalVisitRepository : IMedicalVisitRepository
    {
        public Guid AddMedicalVisit(MedicalVisit medicalVisit)
        {
            return new Guid();
        }
    }
}
