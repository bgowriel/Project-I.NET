using DoctorAppointment.Domain.Models;

namespace DoctorAppointment.Application.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<Appointment?> GetById(Guid id);

        Task<List<Appointment>?> GetAll();

        Task<List<Appointment>?> GetByDoctorId(string doctorId);

        Task<List<Appointment>?> GetByPatientId(string patientId);

        Task<List<Appointment>?> GetByDate(DateTime date);

        Task<List<Appointment>?> GetByDoctorIdAndPatientId(string doctorId, string patientId);

        Task<List<Appointment>?> GetByDoctorIdAndDate(string doctorId, DateTime date);

        Task<List<Appointment>?> GetByPatientIdAndDate(string patientId, DateTime date);

        Task Insert(Appointment appointment);

        void Update(Appointment appointment);

        void Delete(Appointment appointment);
    }
}
