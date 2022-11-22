using DoctorAppointment.Domain.Models;

namespace DoctorAppointment.Application.Interfaces
{
    public interface IBillRepository
    {
        Task<Bill?> GetById(Guid id);

        Task<List<Bill>?> GetAll();

        Task<List<Bill>?> GetByPatientId(string patientId);

        Task<List<Bill>?> GetByDoctorId(string doctorId);

        Task<Bill?> GetByMedicalVisitId(Guid medicalVisitId);

        Task Insert(Bill bill);

        void Update(Bill bill);

        void Delete(Bill bill);
    }
}
