using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.DataAccess.Repositories
{
    public class AvalaibleDateRepository : IAvalaibleDateRepository
    {
        private readonly DatabaseContext context;

        public AvalaibleDateRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<AvalaibleDate?> GetById(Guid id)
        {
            return await context.AvalaibleDates.SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<AvalaibleDate>?> GetAll()
        {
            return await context.AvalaibleDates.Take(100).ToListAsync();
        }

        public async Task Insert(AvalaibleDate avalaibleDate)
        {
            await context.AvalaibleDates.AddAsync(avalaibleDate);
        }

    }
}
