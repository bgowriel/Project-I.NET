using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.Application.MedicalVisitService
{
    public class MedicalVisitService:IMedicalVisitService
    {
        private readonly IMedicalVisitService medicalVisitRepository;

        public MedicalVisitService(IMedicalVisitService medicalVisitRepository)
        {
            this.medicalVisitRepository = medicalVisitRepository;
        }

        public Guid AddMedicalVisit(MedicalVisit medicalVisit)
        {
            return medicalVisitRepository.AddMedicalVisit(medicalVisit);
        }
    }
}
