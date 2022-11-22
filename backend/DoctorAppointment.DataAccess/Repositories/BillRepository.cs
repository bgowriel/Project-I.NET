using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.DataAccess.Repositories
{
    public class BillRepository : IBillRepository
    {
        private readonly DatabaseContext _databaseContext;

        public BillRepository(DatabaseContext context)
        {
            _databaseContext = context;
        }

        public async Task<Bill?> GetById(Guid id)
        {
            return await _databaseContext.Bills.SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Bill>?> GetAll()
        {
            return await _databaseContext.Bills.Take(100).ToListAsync();
        }

        public async Task<List<Bill>?> GetByPatientId(string patientId)
        {
            return await _databaseContext.Bills.Where(b => b.PatientId == patientId)
                                              .Take(100)
                                              .ToListAsync();
        }

        public async Task<List<Bill>?> GetByDoctorId(string doctorId)
        {
            return await _databaseContext.Bills.Where(b => b.DoctorId == doctorId)
                                              .Take(100)
                                              .ToListAsync();
        }

        public async Task<Bill?> GetByMedicalVisitId(Guid medicalVisitId)
        {
            return await _databaseContext.Bills.SingleOrDefaultAsync(b => b.MedicalVisitId == medicalVisitId);
        }

        public async Task Insert(Bill bill)
        {
            await _databaseContext.Bills.AddAsync(bill);
        }

        public void Update(Bill bill)
        {
            _databaseContext.Bills.Update(bill);
        }

        public void Delete(Bill bill)
        {
            _databaseContext.Bills.Remove(bill);
        }
    }
}
