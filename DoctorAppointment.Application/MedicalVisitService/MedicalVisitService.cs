using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.Application.MedicalVisitService
{
    public class MedicalVisitService: IMedicalVisitService
    {
        private readonly IMedicalVisitRepository _medicalVisitRepository;

        public MedicalVisitService(IMedicalVisitRepository medicalVisitRepository)
        {
            _medicalVisitRepository = medicalVisitRepository;
        }

        public Guid AddMedicalVisit(MedicalVisit medicalVisit)
        {
            return _medicalVisitRepository.AddMedicalVisit(medicalVisit);
        }
    }
}
