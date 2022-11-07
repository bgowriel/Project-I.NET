using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.Domain.Interfaces
{
    public interface IMedicalVisitRepository
    {
        public Guid AddMedicalVisit(MedicalVisit medicalVisit);
    }
}
