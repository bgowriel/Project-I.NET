using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.DataAccess.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DatabaseContext _databaseContext;

        public AppointmentRepository(DatabaseContext context)
        {
            _databaseContext = context;
        }

        public async Task<Appointment?> GetById(Guid id)
        {
            return await _databaseContext.Appointments.SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Appointment>?> GetAll()
        {
            return await _databaseContext.Appointments.Take(100).ToListAsync();
        }
        
        public async Task<List<Appointment>?> GetByDoctorId(string doctorId)
        {
            return await _databaseContext.Appointments.Where(a => a.DoctorId == doctorId)
                .Take(100)
                .ToListAsync();
        }

        public async Task<List<Appointment>?> GetByPatientId(string patientId)
        {
            return await _databaseContext.Appointments.Where(a => a.PatientId == patientId)
                                                      .Take(100)
                                                      .ToListAsync();
        }

        public async Task<List<Appointment>?> GetByDate(DateTime date)
        {
            return await _databaseContext.Appointments.Where(a => a.Date == date)
                                                      .Take(100)
                                                      .ToListAsync();
        }

        public async Task<List<Appointment>?> GetByDoctorIdAndPatientId(string doctorId, string patientId)
        {
            return await _databaseContext.Appointments.Where(a => a.DoctorId == doctorId && a.PatientId == patientId)
                                                      .Take(100)
                                                      .ToListAsync();
        }

        public async Task<List<Appointment>?> GetByDoctorIdAndDate(string doctorId, DateTime date)
        {
            return await _databaseContext.Appointments.Where(a => a.DoctorId == doctorId && a.Date == date)
                                                      .Take(100)
                                                      .ToListAsync();
        }

        public async Task<List<Appointment>?> GetByPatientIdAndDate(string patientId, DateTime date)
        {
            return await _databaseContext.Appointments.Where(a => a.PatientId == patientId && a.Date == date)
                                                      .Take(100)
                                                      .ToListAsync();
        }

        public async Task Insert(Appointment appointment)
        {
            await _databaseContext.Appointments.AddAsync(appointment);
        }

        public void Update(Appointment appointment)
        {
            _databaseContext.Appointments.Update(appointment);
        }

        public void Delete(Appointment appointment)
        {
            _databaseContext.Appointments.Remove(appointment);
        }

    }
}
