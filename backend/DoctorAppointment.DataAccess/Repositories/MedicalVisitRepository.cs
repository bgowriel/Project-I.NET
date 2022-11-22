using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.DataAccess.Repositories
{
    public class MedicalVisitRepository : IMedicalVisitRepository
    {
        private readonly DatabaseContext _databaseContext;

        public MedicalVisitRepository(DatabaseContext context)
        {
            _databaseContext = context;
        }

        public async Task<MedicalVisit?> GetById(Guid id)
        {
            return await _databaseContext.MedicalVisits.SingleOrDefaultAsync(mv => mv.Id == id);
        }

        public async Task<List<MedicalVisit>?> GetAll()
        {
            return await _databaseContext.MedicalVisits.Take(100).ToListAsync();
        }

        public async Task<List<MedicalVisit>?> GetByDoctorId(string doctorId)
        {
            return await _databaseContext.MedicalVisits.Where(mv => mv.DoctorId == doctorId)
                                                       .Take(100)
                                                       .ToListAsync();
        }

        public async Task<List<MedicalVisit>?> GetByPatientId(string patientId)
        {
            return await _databaseContext.MedicalVisits.Where(mv => mv.PatientId == patientId)
                                                       .Take(100)
                                                       .ToListAsync();
        }

        public async Task<List<MedicalVisit>?> GetByDate(DateTime date)
        {
            return await _databaseContext.MedicalVisits.Where(mv => mv.Date == date)
                                                       .Take(100)
                                                       .ToListAsync();
        }

        public async Task<List<MedicalVisit>?> GetByDoctorIdAndPatientId(string doctorId, string patientId)
        {
            return await _databaseContext.MedicalVisits.Where(mv => mv.DoctorId == doctorId && mv.PatientId == patientId)
                                                       .Take(100)
                                                       .ToListAsync();
        }

        public async Task<List<MedicalVisit>?> GetByDoctorIdAndDate(string doctorId, DateTime date)
        {
            return await _databaseContext.MedicalVisits.Where(mv => mv.DoctorId == doctorId && mv.Date == date)
                                                       .Take(100)
                                                       .ToListAsync();
        }

        public async Task<List<MedicalVisit>?> GetByPatientIdAndDate(string patientId, DateTime date)
        {
            return await _databaseContext.MedicalVisits.Where(mv => mv.PatientId == patientId && mv.Date == date)
                                                       .Take(100)
                                                       .ToListAsync();
        }

        public async Task Insert(MedicalVisit medicalVisit)
        {
            await _databaseContext.MedicalVisits.AddAsync(medicalVisit);
        }

        public void Update(MedicalVisit medicalVisit)
        {
            _databaseContext.MedicalVisits.Update(medicalVisit);
        }

        public void Delete(MedicalVisit medicalVisit)
        {
            _databaseContext.MedicalVisits.Remove(medicalVisit);
        }
    }
}
