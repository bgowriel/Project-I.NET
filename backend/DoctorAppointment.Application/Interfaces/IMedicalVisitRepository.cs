using DoctorAppointment.Domain.Models;

namespace DoctorAppointment.Application.Interfaces
{
    public interface IMedicalVisitRepository
    {
        Task<MedicalVisit?> GetById(Guid id);

        Task<List<MedicalVisit>?> GetAll();

        Task<List<MedicalVisit>?> GetByPatientId(string patientId);

        Task<List<MedicalVisit>?> GetByDoctorId(string doctorId);

        Task<List<MedicalVisit>?> GetByDate(DateTime date);

        Task<List<MedicalVisit>?> GetByDoctorIdAndPatientId(string doctorId, string patientId);

        Task<List<MedicalVisit>?> GetByDoctorIdAndDate(string doctorId, DateTime date);

        Task<List<MedicalVisit>?> GetByPatientIdAndDate(string patientId, DateTime date);

        Task Insert(MedicalVisit medicalVisit);

        void Update(MedicalVisit medicalVisit);

        void Delete(MedicalVisit medicalVisit);
    }
}
