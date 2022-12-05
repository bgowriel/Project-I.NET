using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.DataAccess.Repositories
{
    public class AvailableDateRepository : IAvailableDateRepository
    {
        private readonly DatabaseContext context;

        public AvailableDateRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<AvailableDate?> GetById(Guid id)
        {
            return await context.AvalaibleDates.SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<AvailableDate>?> GetAll()
        {
            return await context.AvalaibleDates.Take(100).ToListAsync();
        }

        public async Task Insert(AvailableDate avalaibleDate)
        {
            await context.AvalaibleDates.AddAsync(avalaibleDate);
        }

    }
}
